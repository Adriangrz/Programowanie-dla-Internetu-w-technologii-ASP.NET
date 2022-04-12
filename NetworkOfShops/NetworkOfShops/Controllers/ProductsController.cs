#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetworkOfShops.Data;
using NetworkOfShops.Models;
using NetworkOfShops.Repositories;
using NetworkOfShops.Repositories.Interfaces;

namespace NetworkOfShops.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _repositoryProduct;
        private readonly IGenericRepository<Shop> _repositoryShop;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> repositoryProduct, IGenericRepository<Shop> repositoryShop, IMapper mapper)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryShop = repositoryShop;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _repositoryProduct.Get(null,null,"Shop");
            var resources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return View(resources);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repositoryProduct.GetBy(m => m.Id == id, "Shop");
            if (product == null)
            {
                return NotFound();
            }

            var resource = _mapper.Map<Product, ProductViewModel>(product);
            return View(resource);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ShopId"] = new SelectList(await _repositoryShop.Get(), "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ShopId")] ProductCreateOrEditViewModel productViewModel)
        {
            var product = _mapper.Map<ProductCreateOrEditViewModel, Product>(productViewModel);
            if (ModelState.IsValid)
            {
                await _repositoryProduct.Insert(product);
                await _repositoryProduct.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ShopId"] = new SelectList(await _repositoryShop.Get(), "Id", "Id", product.ShopId);
            var resource = _mapper.Map<Product, ProductCreateOrEditViewModel>(product);
            return View(resource);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repositoryProduct.GetBy(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ShopId"] = new SelectList(await _repositoryShop.Get(), "Id", "Id", product.ShopId);
            var resource = _mapper.Map<Product, ProductCreateOrEditViewModel>(product);
            return View(resource);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ShopId")] ProductCreateOrEditViewModel productViewModel)
        {
            var product = _mapper.Map<ProductCreateOrEditViewModel, Product>(productViewModel);
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repositoryProduct.Update(product);
                    await _repositoryProduct.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ProductExists(product.Id))
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
            ViewData["ShopId"] = new SelectList(await _repositoryShop.Get(), "Id", "Id", product.ShopId);
            var resource = _mapper.Map<Product, ProductCreateOrEditViewModel>(product);
            return View(resource);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _repositoryProduct.GetBy(m => m.Id == id, "Shop");
            if (product == null)
            {
                return NotFound();
            }
            var resource = _mapper.Map<Product, ProductViewModel>(product);
            return View(resource);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _repositoryProduct.GetBy(m => m.Id == id);
            _repositoryProduct.Delete(product);
            await _repositoryProduct.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _repositoryProduct.GetBy(m => m.Id == id) == null;
        }
    }
}
