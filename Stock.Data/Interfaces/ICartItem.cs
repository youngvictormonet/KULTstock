using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Data.Models;
namespace Stock.Data.Interfaces
{
    public interface ICartItem
    {
        CartItem GetByID(int id);
        IEnumerable<CartItem> GetAll();

        Task Add(CartItem cartItem);
        Task Delete(int id);
    }
}
