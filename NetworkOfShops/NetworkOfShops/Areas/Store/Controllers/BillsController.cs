#nullable disable
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
    public class BillsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<AplicationUser> _userManager;

        public BillsController(AplicationDbContext context, UserManager<AplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Store/Bills
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            var aplicationDbContext = _context.Bill.Where(p => p.ShopId == shop.Id).Include(b => b.Shop);
            return View(await aplicationDbContext.ToListAsync());
        }
        [Route("/store/bills/MonthlySalesStatement")]
        public async Task<IActionResult> MonthlySalesStatement()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            var aplicationDbContext = _context.Bill.Where(p => p.ShopId == shop.Id && p.CreationDate.Month == DateTime.Now.Month).Include(x=>x.ProductsInBill);
            ViewData["NumberOfProducts"] = aplicationDbContext.Sum(x=>x.ProductsInBill.Sum(x => x.Amount));
            ViewData["AmountOfMoney"] = aplicationDbContext.Sum(x => x.ToPay);
            return View(await aplicationDbContext.ToListAsync());
        }

        public JsonResult FetchAnnualAmountOfMoney()
        {
            var aplicationDbContext = _context.Bill.Where(p => p.CreationDate.Year == DateTime.Now.Year).Include(x => x.ProductsInBill);
            var sum = aplicationDbContext.Sum(x => x.ToPay);
            return Json(sum);
        }

        [Route("/store/bills/AnnualSalesStatement")]
        public async Task<IActionResult> AnnualSalesStatement()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            var aplicationDbContext = _context.Bill.Where(p => p.ShopId == shop.Id && p.CreationDate.Year == DateTime.Now.Year).Include(x => x.ProductsInBill);
            ViewData["NumberOfProducts"] = aplicationDbContext.Sum(x => x.ProductsInBill.Sum(x => x.Amount));
            ViewData["AmountOfMoney"] = aplicationDbContext.Sum(x => x.ToPay);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: Store/Bills/Details/5
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "id" })]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.Shop)
                .Include(b => b.ProductsInBill)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        [Route("/store/bills/{id:int}/GenerateInvoice")]
        public async Task<IActionResult> GenerateInvoice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.Shop)
                .Include(b => b.ProductsInBill).ThenInclude(productsInBill => productsInBill.ProductInShop).ThenInclude(roductInShop => roductInShop.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        [Route("/store/bills/{id:int}/ProductInBill")]
        public IActionResult CreateProductInBill(int? id)
        {
            var userId = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == userId);
            ViewData["ProductInShopId"] = new SelectList(_context.ProductsInShop.Where(p => p.ShopId == shop.Id), "Id","Id");
            ViewData["BillId"] = id;
            return View();
        }
        [Route("/store/bills/{id:int}/ProductInBill")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProductInBill([Bind("Amount,ProductInShopId,BillId")] ProductInBill productInBill)
        {
            if (ModelState.IsValid)
            {
                productInBill.Price = productInBill.Amount * _context.ProductsInShop.FirstOrDefault(p => p.Id == productInBill.ProductInShopId).PriceInShop;
                _context.Add(productInBill);
                var bill = _context.Bill.FirstOrDefault(p => p.Id == productInBill.BillId);
                bill.ToPay += productInBill.Price;
                _context.Update(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var userId = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == userId);
            ViewData["ProductInShopId"] = new SelectList(_context.ProductsInShop.Where(p => p.ShopId == shop.Id), "Id", "Id",productInBill.ProductInShopId);
            ViewData["BillId"] = productInBill.BillId;
            return View(productInBill);
        }

        // GET: Store/Bills/Create
        public IActionResult Create()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            ViewData["ShopId"] = shop.Id;
            return View();
        }

        // POST: Store/Bills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreationDate,ToPay,ShopId")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = bill.ShopId;
            return View(bill);
        }

        // GET: Store/Bills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = bill.ShopId;
            return View(bill);
        }

        // POST: Store/Bills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreationDate,ToPay,ShopId")] Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillExists(bill.Id))
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
            ViewData["ShopId"] = bill.ShopId;
            return View(bill);
        }

        // GET: Store/Bills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _context.Bill
                .Include(b => b.Shop)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Store/Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bill = await _context.Bill.FindAsync(id);
            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id == id);
        }
    }
}
