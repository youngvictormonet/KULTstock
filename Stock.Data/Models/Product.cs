using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Stock.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageURL { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public string Accepted { get; set; }
    }
}
