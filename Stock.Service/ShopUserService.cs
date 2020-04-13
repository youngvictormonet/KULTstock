using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stock.Data.Interfaces;
using Stock.Data.Models;
using Stock.Data;
using Microsoft.AspNetCore.Identity;
namespace Stock.Service
{
    public class ShopUserService : IUser
    {
        private readonly WebContext _context;

        public ShopUserService(WebContext context)
        {
            _context = context;
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return _context.ShopUsers;
        }

        public IdentityUser GetById(string id)
        {
            return _context.ShopUsers.FirstOrDefault(user => user.Id == id);
        }
    }
}
