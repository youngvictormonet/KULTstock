using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Data;
using Stock.Data.Models;
namespace KultStock.Data
{
    public class DataBaseInitializer
    {
        public static void Initialize(WebContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }

            var products = new Product[]
            {
        new Product{ Name = "Item1", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item2", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item3", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item4", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item5", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item6", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item7", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item8", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item9", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item10", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item11", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item12", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item13", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item14", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item15", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item16", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item17", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item18", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item19", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},
        new Product{ Name = "Item20", Description = "Powerfull and effective product for you business. With help of this item you can achive everything you want. You'll be happy for sure.", ImageURL = "https://i.imgur.com/ydxzeqI.png", Price = 245.55m},

            };

            foreach (Product product in products)
            {
                context.Add(product);
            }
            context.SaveChanges();
        }
    }
}

