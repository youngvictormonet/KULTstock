using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Data.Models;
namespace Stock.Data.Interfaces
{
    public interface ICart
    {
        Cart GetByID(int id);
        IEnumerable<Cart> GetAll();

        //User ID
        IEnumerable<CartItem> GetItems(string id);

        Cart GetByUserID(string id);
        Task Add(Cart cart);
        Task AddCartItem(CartItem cartItem);
        Task DeleteCartItem(CartItem cartItem);
        Task DeleteItemFromCarttt(Product product, string id);
        Task AddItemToCart(Product product, string id);

        int DeleteItemFromCart(Product product, string id);
        int DeleteItemFromCartAtAll(CartItem cartItem, string id);

        void Clear(string id);
        decimal GetTotal(string id);

        Task Delete(int id);
    }
}
