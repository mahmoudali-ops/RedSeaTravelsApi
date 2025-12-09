using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Servicies.Contract
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(User user,UserManager<User> userManager);
    }
}
