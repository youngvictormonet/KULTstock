using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KultStock.ViewModels.Product
{
    public class SearchIndexModel
    {
        public IEnumerable<ProductListingModel> ProductList { get; set; }
        public string SearchQuery { get; set; }
    }
}
