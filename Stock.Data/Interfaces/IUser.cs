using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Stock.Data.Interfaces
{
    public interface IUser
    {
        IdentityUser GetById(string id);
        IEnumerable<IdentityUser> GetAll();
    }
}
