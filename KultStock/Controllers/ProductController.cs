using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KultStock.ViewModels;
using KultStock.ViewModels.Product;
using Stock.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using KultStock.Data;
using Stock.Data;
using Stock.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace KultStock.Controllers
{
        public class ProductController : Controller
        {
            private readonly WebContext _context;
            private readonly IProduct _productService;
            private readonly ICart _cartService;

            private static UserManager<IdentityUser> _userManager;

            public ProductController(WebContext context, IProduct productService, ICart cartService, UserManager<IdentityUser> userManager)
            {
                _context = context;
                _productService = productService;
                _cartService = cartService;
                _userManager = userManager;
            }

            public IActionResult Index()
            {
                IEnumerable<ProductListingModel> products = _productService.GetAll().Select(product => new ProductListingModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageURL = product.ImageURL,
                    Price = product.Price
                });

                ProductIndexModel model = new ProductIndexModel
                {
                    ProductList = products
                };

                return View(model);
            }

            [HttpGet]
            public IActionResult Search(string searchQuery)
            {
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    var products = _productService.GetAllFiltered(searchQuery);
                    var productListing = products.Select(product => new ProductListingModel
                    {
                        Id = product.Id,
                        Name = product.Name,
                        ImageURL = product.ImageURL,
                        Price = product.Price
                    });

                    var model = new SearchIndexModel
                    {
                        ProductList = productListing,
                        SearchQuery = searchQuery
                    };
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            public IActionResult Details(int id)
            {
                var product = _productService.GetByID(id);

                var model = new ProductDetailModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    ImageURL = product.ImageURL,
                    Price = product.Price,
                    Description = product.Description
                };
                return View(model);
            }

            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }

            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }

            [Authorize(Roles = "admin")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,ImageURL,Price")] Product product)
            {
                if (id != product.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
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
                return View(product);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            public IActionResult Create()
            {
                return View();
            }

            [Authorize(Roles = "admin")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("ID,Name,Description,ImageURL,Price")] Product product)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }

            private bool ProductExists(int id)
            {
                return _context.Products.Any(e => e.Id == id);
            }
        }
    }

