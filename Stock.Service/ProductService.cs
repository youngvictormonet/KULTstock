using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Stock.Data.Models;
using Stock.Data;
using Stock.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Service
{
    public class ProductService:IProduct
    {
        private readonly WebContext _context;
        private readonly IUser _userService;

        public ProductService(WebContext context, IUser userService)
        {
            _context = context;
            _userService = userService;
        }

        public Product GetByID(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products;
        }

        public IEnumerable<Product> GetAllFiltered(string searchQuery)
        {
            return GetAll().Where(product => (product.Name!=null&& product.Name.ToLower().Contains(searchQuery.ToLower())) 
            || (product.Description != null && product.Description.ToLower().Contains(searchQuery.ToLower()))
            || (product.Type != null && product.Type.ToLower().Contains(searchQuery.ToLower()))
            || (product.Style != null && product.Style.ToLower().Contains(searchQuery.ToLower())));
        }


        public async Task Add(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = GetByID(id);
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}

