using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stock.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace Stock.Data
{
    public class WebContext : IdentityDbContext<IdentityUser>
    {
        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }
        public DbSet<IdentityUser> ShopUsers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Cart> Carts { get; set; }
    }
}
