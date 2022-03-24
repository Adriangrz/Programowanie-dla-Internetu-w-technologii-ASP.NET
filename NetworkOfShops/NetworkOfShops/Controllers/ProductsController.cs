#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Data;
using NetworkOfShops.Models;
using NetworkOfShops.Repositories;
using NetworkOfShops.Repositories.Interfaces;

namespace NetworkOfShops.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _repository.Get(null,null,"Shop");
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return View(resources);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var product = await _context.Products
            //    .Include(p => p.Shop)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}

            //var resource = _mapper.Map<Product, ProductViewModel>(product);
            //return View(resource);
            return View();
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Id");
            //return View();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ShopId")] ProductCreateOrEditViewModel productViewModel)
        {
            //var product = _mapper.Map<ProductCreateOrEditViewModel, Product>(productViewModel);
            //if (ModelState.IsValid)
            //{
            //    _context.Add(product);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Id", product.ShopId);
            //var resource = _mapper.Map<Product, ProductCreateOrEditViewModel>(product);
            //return View(resource);
            return View();
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var product = await _context.Products.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Id", product.ShopId);
            //var resource = _mapper.Map<Product, ProductCreateOrEditViewModel>(product);
            //return View(resource);
            return View();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ShopId")] ProductCreateOrEditViewModel productViewModel)
        {
            //var product = _mapper.Map<ProductCreateOrEditViewModel, Product>(productViewModel);
            //if (id != product.Id)
            //{
            //    return NotFound();
            //}

            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(product);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductExists(product.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ShopId"] = new SelectList(_context.Shops, "Id", "Id", product.ShopId);
            //var resource = _mapper.Map<Product, ProductCreateOrEditViewModel>(product);
            //return View(resource);
            return View();
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var product = await _context.Products
            //    .Include(p => p.Shop)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            //var resource = _mapper.Map<Product, ProductViewModel>(product);
            //return View(resource);
            return View();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var product = await _context.Products.FindAsync(id);
            //_context.Products.Remove(product);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            //return _context.Products.Any(e => e.Id == id);
            return true;
        }
    }
}
