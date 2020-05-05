using Microsoft.Extensions.Options;
using PayPal.Api;
using PayPalExpress.ConfigOptions;
using PayPalExpress.Interfaces;
using System;
using System.Collections.Generic;

using Stock.Data;
using System.Linq;

namespace PayPalExpress
{
    public class PaypalServices : IPaypalServices
    {
        private readonly PayPalAuthOptions _options;
        //private readonly WebContext _context;
        public PaypalServices(IOptions<PayPalAuthOptions> options)
        {
            //_context = context;
            _options = options.Value;
            
        }

        public Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent)
        {
           // var UserID = _userManager.GetUserId(User);
           //var cart=_context.Carts.FirstOrDefault(x => x.ShopUser.Equals(id));

            var apiContext = new APIContext(new OAuthTokenCredential(_options.PayPalClientId, _options.PayPalClientSecret).GetAccessToken());

            var payment = new Payment()
            {
                intent = intent,
                payer = new Payer() { payment_method = "paypal" },
                transactions = GetTransactionsList(amount),
                redirect_urls = new RedirectUrls()
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                }
            };

            var createdPayment = payment.Create(apiContext);

            return createdPayment;
        }


        private List<Transaction> GetTransactionsList(decimal amount)
        {
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = GetRandomInvoiceNumber(),
                amount = new Amount()
                {
                    currency = "USD",
                    total = amount.ToString(),
                    details = new Details()
                    {
                        tax = "0",
                        shipping = "0",
                        subtotal = amount.ToString()
                    }
                },
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Payment",
                            currency = "USD",
                            price = amount.ToString(),
                            quantity = "1",
                            sku = "sku"
                        }
                    }
                },
                payee = new Payee
                {
                    // TODO.. Enter the payee email address here
                    email = "sb-1stje1580660@personal.example.com",

                    // TODO.. Enter the merchant id here
                    merchant_id = "LQT8RQURBR5CC"
                }
            });

            return transactionList;
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(_options.PayPalClientId, _options.PayPalClientSecret).GetAccessToken());

            var paymentExecution = new PaymentExecution() { payer_id = payerId };

            var executedPayment = new Payment() { id = paymentId }.Execute(apiContext, paymentExecution);

            return executedPayment;
        }

        private string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999999).ToString();
        }
    }
}
