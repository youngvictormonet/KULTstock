using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KultStock.ViewModels;
using KultStock.ViewModels.Cart;
using Stock.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Stock.Data;
using Stock.Data.Interfaces;
namespace KultStock.Controllers
{
    public class CartController : Controller
    {
        private readonly WebContext _context;
        private readonly IProduct _productService;
        private readonly ICartItem _cartItemService;
        private readonly ICart _cartService;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(WebContext context, IProduct productService, ICartItem cartItemService, ICart cartService, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _productService = productService;
            _cartItemService = cartItemService;
            _cartService = cartService;
            _userManager = userManager;
        }

        [Authorize]
        public IActionResult Index()
        {
            var UserID = _userManager.GetUserId(User);
            IEnumerable<CartListingModel> cartItems = _cartService.GetByUserID(UserID).CartItems.Select(cartItem => new CartListingModel
            {
                CartItemId = cartItem.CartItemId,
                ProductId = cartItem.Product.Id,
                Name = cartItem.Product.Name,
                ImageURL = cartItem.Product.ImageURL,
                Price = cartItem.Product.Price,
                Amount = cartItem.Amount,
            });

            CartIndexModel model = new CartIndexModel
            {
                CartItemsList = cartItems,
                Total = _cartService.GetTotal(UserID)
            };

            return View(model);
        }

        [Authorize]
        //[HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _productService.GetByID(id);
            var userId = _userManager.GetUserId(User);
            _cartService.AddItemToCart(product, userId);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var product = _productService.GetByID(id);
            var UserID = _userManager.GetUserId(User);
            _cartService.DeleteItemFromCart(product, UserID);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCartAtAll(int id)
        {
            var cartItem = _cartItemService.GetByID(id);
            var UserID = _userManager.GetUserId(User);
            _cartService.DeleteItemFromCartAtAll(cartItem, UserID);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveAllItems()
        {
            var UserID = _userManager.GetUserId(User);
            _cartService.Clear(UserID);
            return RedirectToAction("Index");
        }

        public decimal Total()
        {
            var userId = _userManager.GetUserId(User);
            return _cartService.GetTotal(userId);
        }
    }
}
