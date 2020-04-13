using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Data.Models;
namespace Stock.Data.Interfaces
{
    public interface IProduct
    {
        Product GetByID(int id);
        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetAllFiltered(string searchQuery);

        Task Add(Product product);
        Task Delete(int id);
    }
}
