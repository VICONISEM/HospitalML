using Hospital.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.BLL.TokenServices
{
    public interface ITokenServices
    {
        public Task<string> CreateTokenAsync(ApplicationUser User, UserManager<ApplicationUser> userManager);
    }
}
