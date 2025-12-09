using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Errors;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Tours;
using TourSite.Core.Specification.Users;

namespace TourSite.APIs.Controllers
{
    
    public class UserController : BaseApiController
    {
        private readonly IUserService usersService;
        public UserController(IUserService _toursService)
        {
            usersService = _toursService;
        }
        [HttpGet]
    //    [Authorize]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserSpeciParams userSpecParams)
        {
            var users = await usersService.GetAllUserAsync(userSpecParams);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var user = await usersService.GetUserByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no User with this Id : {id}"));
            }
            return Ok(user);
        }
    }
}
