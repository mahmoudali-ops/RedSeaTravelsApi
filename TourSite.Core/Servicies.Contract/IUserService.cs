using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.DTOs.User;
using TourSite.Core.Specification.Tours;
using TourSite.Core.Specification.Users;

namespace TourSite.Core.Servicies.Contract
{
    public interface IUserService
    {
        Task<PageinationResponse<UserDto>> GetAllUserAsync(UserSpeciParams userSpecParams);
        Task<UserDto> GetUserByIdAsync(string id);
    }
}
