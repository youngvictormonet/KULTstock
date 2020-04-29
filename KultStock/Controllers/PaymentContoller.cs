﻿
using Microsoft.AspNetCore.Mvc;
using PayPalExpress.Interfaces;
namespace KultStock.Controllers
{
    public class PaymentController:Controller
    {
        private IPaypalServices _PaypalServices;

        public PaymentController(IPaypalServices paypalServices)
        {
            _PaypalServices = paypalServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreatePayment()
        {
            var payment = _PaypalServices.CreatePayment(100, "http://localhost:52095/Payment/ExecutePayment", "http://localhost:52095/Payment/Cancel", "sale");

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
