using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.User;
using TourSite.Core.DTOs.UserAuth;
using TourSite.Core.Entities;

namespace TourSite.Core.Servicies.Contract
{
    public interface IAuthService
    {
        Task<UserAuthDto> RegisterAsync(RegisterAuthDto registerAuthDto);
        Task<UserAuthDto> LoginAsync(LoginAuthDto loginAuthDto);
        Task<bool> CheckEmail(string email);
    }
}
