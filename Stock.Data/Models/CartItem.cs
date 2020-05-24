using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Data.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public virtual Product Product { get; set; }

        public int Amount { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
