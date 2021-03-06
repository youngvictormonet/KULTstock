﻿using System;
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
using Microsoft.AspNetCore.Mvc.Rendering;

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
                    Price = product.Price,
                    Type = product.Type,
                    Style = product.Style,
                    Country = product.Country,
                    City = product.City,
                    Adress = product.Adress,
                    Date = product.Date,
                    Time = product.Time,
                    Age = product.Age,
                    Accepted =product.Accepted
                });

                ProductIndexModel model = new ProductIndexModel
                {
                    ProductList = products
                };

                return View(model);
            }

            [HttpGet]
            public IActionResult All()
            {
            ViewBag.Type = _context.Products.GroupBy(x => x.Type).Select(i => new
              SelectListItem()
            {
                Text = i.Key,
                Value = i.Key
            }).ToList();

            ViewBag.Style = _context.Products.GroupBy(x => x.Style).Select(i => new
                 SelectListItem()
            {
                Text = i.Key,
                Value = i.Key
            }).ToList();

            var products = _context.Products;   

            return View(products.OrderBy(x => x.Date).ToList());
            }

        [HttpPost]
        public IActionResult All(string Type,string Style,string Price)
        {

            ViewBag.Type = _context.Products.GroupBy(x=>x.Type).Select(i => new
            SelectListItem()
            {
                Text = i.Key,
                Value = i.Key
            }).ToList();

            ViewBag.Style = _context.Products.GroupBy(x => x.Style).Select(i => new
            SelectListItem()
            {
                Text = i.Key,
                Value = i.Key
            }).ToList();

            List<Product>products=new List<Product>();
            if (Price == "1")
            {
                if (Type != null && Style != null)
                {
                    products = _context.Products.Where(
                        x => (x.Type == Type || x.Style == Type) &&
                        (x.Style == Style)
                        ).OrderByDescending(x=>x.Price).ThenBy(x => x.Date).ToList();
                }
                else if (Type != null)
                {
                    products = _context.Products.Where(
                      x => (x.Type == Type || x.Style == Type)
                      ).OrderByDescending(x => x.Price).ThenBy(x => x.Date).ToList();
                }
                else
                {
                    products = _context.Products.Where(
                      x => (x.Style == Style)
                      ).OrderByDescending(x => x.Price).ThenBy(x => x.Date).ToList();
                }
            }
            else
            {
                if (Type != null && Style != null)
                {
                    products = _context.Products.Where(
                        x => (x.Type == Type || x.Style == Type) &&
                        (x.Style == Style)
                        ).OrderBy(x => x.Price).ThenBy(x => x.Date).ToList();
                }
                else if (Type != null)
                {
                    products = _context.Products.Where(
                      x => (x.Type == Type || x.Style == Type)
                      ).OrderBy(x => x.Price).ThenBy(x => x.Date).ToList();
                }
                else
                {
                    products = _context.Products.Where(
                      x => (x.Style == Style)
                      ).OrderBy(x => x.Price).ThenBy(x=>x.Date).ToList();
                }
            }
            return View(products.ToList());
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
                        Price = product.Price,
                        Type = product.Type,
                        Style = product.Style,
                        Country = product.Country,
                        City = product.City,
                        Adress = product.Adress,
                        Date = product.Date,
                        Time = product.Time,
                        Age = product.Age,
                        Accepted =product.Accepted
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
                    Type = product.Type,
                    Style = product.Style,
                    Country = product.Country,
                    City = product.City,
                    Adress = product.Adress,
                    Date = product.Date,
                    Time = product.Time,
                    Age = product.Age,
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

            [HttpPost]
            public async Task<IActionResult> Edit( Product product)
            {
            product.Accepted = _context.Products.Where(a => a.Id == product.Id).Select(p => p.Accepted).SingleOrDefault();
                 _context.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Product");
            
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

        [HttpGet]
        public IActionResult Find()
        {

            IEnumerable<ProductListingModel> products = _productService.GetAll().Select(product => new ProductListingModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Type = product.Type,
                Style = product.Style,
                Country = product.Country,
                City = product.City,
                Adress = product.Adress,
                Date = product.Date,
                Time = product.Time,
                Age = product.Age,
                Accepted = product.Accepted
            });

            ProductIndexModel model = new ProductIndexModel
            {
                ProductList = products
            };



            return View(model);
        }
        [HttpPost]
        public IActionResult Find(ProductIndexModel model)
        {


            

            return View(model);
        }

        public async Task <IActionResult> Add(int id)
        {
            var product = _productService.GetByID(id);
            product.Accepted = "1";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product"); 
        }

            //[Authorize(Roles = "admin")]
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("ID,Name,Description,ImageURL,Price,Type,Style,Country,City,Adress,Date,Time,Age,Accepted")] Product product)
            {
            if (User.IsInRole("admin"))
            {
                if (ModelState.IsValid)
                {
                    product.Accepted = "1";
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    product.Accepted = "0";
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(product);
            }
            }

            private bool ProductExists(int id)
            {
                return _context.Products.Any(e => e.Id == id);
            }
        }
    }

