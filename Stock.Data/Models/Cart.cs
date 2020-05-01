using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Stock.Data.Models
{
    public class Cart
    {
        public int CartId { get; set; }

        public virtual IdentityUser ShopUser { get; set; }

        public decimal Total { get; set; }
        public virtual IEnumerable<CartItem> CartItems { get; set; }
    }
}
