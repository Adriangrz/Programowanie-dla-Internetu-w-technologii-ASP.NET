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
using NetworkOfShops.Areas.Store.Models;
using NetworkOfShops.Data;
using NetworkOfShops.Models;

namespace NetworkOfShops.Areas.Store.Controllers
{
    [Area("Store")]
    [Authorize]
    public class ProductInShopsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly UserManager<AplicationUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductInShopsController(AplicationDbContext context, UserManager<AplicationUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Store/ProductInShops
        public async Task<IActionResult> Index()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            var aplicationDbContext = _context.ProductsInShop.Where(p => p.ShopId == shop.Id);
            return View(await _context.ProductsInShop.ToListAsync());
        }

        // GET: Store/ProductInShops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShop = await _context.ProductsInShop
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInShop == null)
            {
                return NotFound();
            }

            return View(productInShop);
        }

        // GET: Store/ProductInShops/Create
        public IActionResult Create()
        {
            var id = _userManager.GetUserId(User);
            var shop = _context.Shops.FirstOrDefault(s => s.UserId == id);
            ViewData["ShopId"] = shop.Id;
            ViewData["ProductsId"] = new SelectList(_context.Products,"Id","Id");
            return View();
        }

        // POST: Store/ProductInShops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,ShopId,PriceInShop")] ProductInShop productInShop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productInShop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productInShop);
        }

        // GET: Store/ProductInShops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShop = await _context.ProductsInShop.FindAsync(id);
            if (productInShop == null)
            {
                return NotFound();
            }
            ViewData["ProductsId"] = new SelectList(_context.Products, "Id", "Id");
            return View(productInShop);
        }

        // POST: Store/ProductInShops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,ShopId,PriceInShop")] ProductInShop productInShop)
        {
            if (id != productInShop.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInShop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInShopExists(productInShop.Id))
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
            return View(productInShop);
        }

        public async Task<IActionResult> AddFile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShop = await _context.ProductsInShop.FindAsync(id);
            if (productInShop == null)
            {
                return NotFound();
            }
            var imageFileAddViewModel = new ImageFileAddViewModel()
            {
                Id = (int)id
            };
            return View(imageFileAddViewModel);
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFile(int id, [Bind("Id,ProductImage")] ImageFileAddViewModel imageFileAddViewModel)
        {
            if (id != imageFileAddViewModel.Id)
            {
                return NotFound();
            }
            var productInShop = await _context.ProductsInShop.FindAsync(imageFileAddViewModel.Id);
            if (productInShop == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = null;

                if (imageFileAddViewModel.ProductImage != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFileAddViewModel.ProductImage.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    imageFileAddViewModel.ProductImage.CopyTo(new FileStream(filePath, FileMode.Create));
                    productInShop.ProductImage = uniqueFileName;
                }
            }

            try
            {
                _context.Update(productInShop);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInShopExists(productInShop.Id))
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

        // GET: Store/ProductInShops/Delete/5
        [ResponseCache(Duration = 120, VaryByQueryKeys = new[] { "id" })]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShop = await _context.ProductsInShop
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInShop == null)
            {
                return NotFound();
            }

            return View(productInShop);
        }

        // POST: Store/ProductInShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productInShop = await _context.ProductsInShop.FindAsync(id);
            _context.ProductsInShop.Remove(productInShop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInShopExists(int id)
        {
            return _context.ProductsInShop.Any(e => e.Id == id);
        }
    }
}
