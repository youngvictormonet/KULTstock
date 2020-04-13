using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stock.Data.Interfaces;
using Stock.Data;
using System.Threading.Tasks;
using Stock.Data.Models;
namespace Stock.Service
{
    public class CartItemService : ICartItem
    {
        private readonly WebContext _context;

        public CartItemService(WebContext context)
        {
            _context = context;
        }

        public async Task Add(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var cartItem = GetByID(id);
            _context.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<CartItem> GetAll()
        {
            return _context.CartItems
                .Include(cartItem => cartItem.Product);
        }

        public CartItem GetByID(int id)
        {
            return _context.CartItems.Where(cartItem => cartItem.CartItemId == id).SingleOrDefault();
        }
    }
}

