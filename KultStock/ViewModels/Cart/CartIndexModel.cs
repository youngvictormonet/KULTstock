using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KultStock.ViewModels.Cart
{
    public class CartIndexModel
    {
        public IEnumerable<CartListingModel> CartItemsList { get; set; }

        public decimal Total { get; set; }
    }
}
