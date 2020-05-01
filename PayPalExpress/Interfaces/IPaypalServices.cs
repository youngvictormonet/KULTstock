using System;
using System.Collections.Generic;
using System.Text;
using PayPal.Api;
namespace PayPalExpress.Interfaces
{
    public interface IPaypalServices
    {
        Payment CreatePayment(string id, string returnUrl, string cancelUrl, string intent);

        Payment ExecutePayment(string paymentId, string payerId);
    }
}
