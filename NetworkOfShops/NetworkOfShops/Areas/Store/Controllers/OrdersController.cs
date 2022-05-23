﻿#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Data;
using NetworkOfShops.Models;

namespace NetworkOfShops.Areas.Store.Controllers
{
    [Area("Store")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<AplicationUser> _userManager;

        public OrdersController(AplicationDbContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Store/Orders
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            var aplicationDbContext = _context.Orders.Where(p => p.ShopId == shop.Id).Include(o => o.Shop);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: Store/Orders/Details/5
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "id" })]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Store/Orders/Create
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Description");
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>());
            return View();
        }

        // POST: Store/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShopId,CreationDate,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Description", order.ShopId);
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>());
            return View(order);
        }

        // GET: Store/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Description", order.ShopId);
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>(), order.Status);
            return View(order);
        }

        // POST: Store/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShopId,CreationDate,Status")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Description", order.ShopId);
            ViewData["Status"] = new SelectList(Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>(), order.Status);
            return View(order);
        }

        // GET: Store/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Store/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
