using KultStock.ViewModels.Cart;
using Stock.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PayPalExpress.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using Newtonsoft.Json;
using Stock.Data;
using Stock.Data.Models;
using System.Linq;
namespace KultStock.Controllers
{
    public class PaymentController:Controller
    {
        private IPaypalServices _PaypalServices;
        private readonly WebContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public PaymentController(IPaypalServices paypalServices, WebContext context, UserManager<IdentityUser> userManager)
        {
            _PaypalServices = paypalServices;
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePayment()
        {
            //var UserID = _userManager.GetUserId(User);
            //var Total = _cart.GetTotal(UserID);
            //CartIndexModel cart = TempData["mydata"] as CartIndexModel;
            //var story = JsonConvert.DeserializeObject<CartIndexModel>(TempData["model"].ToString());
            //decimal amount = 100;
            var UserID = _userManager.GetUserId(User);
            //var cart=_context.Carts.FirstOrDefault(x => x.ShopUser.Equals(UserID));
            var payment = _PaypalServices.CreatePayment(UserID, "https://localhost:44300/Payment/ExecutePayment", "https://localhost:44300/Payment/Cancel", "sale");

            return new JsonResult(payment);
        }

        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            var payment = _PaypalServices.ExecutePayment(paymentId, PayerID);

            // Hint: You can save the transaction details to your database using payment/buyer info

            return Ok();
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }

    }
}
