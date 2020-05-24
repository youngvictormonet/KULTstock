using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KultStock.ViewModels.Product
{
    public class ProductListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Adress { get; set; }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public string Age { get; set; }
        public string Accepted { get; set; }
    }
}
