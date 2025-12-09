using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.User;
using TourSite.Core.DTOs.UserAuth;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Service.Services.Auth;
using TourSite.Service.Services.Token;

namespace TourSite.APIs.Controllers
{
   
    public class AccountController : BaseApiController
    {
        private readonly IAuthService authService;
        private readonly UserManager<User> userManager;

        public AccountController(IAuthService _authService,UserManager<User> _userManager,ITokenService tokenService)
        {
            authService = _authService;
           userManager = _userManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserAuthDto>> Register(RegisterAuthDto registerAuthDto )
        {
            var result = await authService.RegisterAsync(registerAuthDto);
            if (result == null) return BadRequest(new APIErrerResponse(400, "Invalid registeration"));

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthDto>> Login(LoginAuthDto loginAuthDto)
        {
            
            var result = await authService.LoginAsync(loginAuthDto);



            if (result == null)
                return Unauthorized(new APIErrerResponse(401, "Invalid email or password"));

            // ============== 1) Create HttpOnly Cookie =================
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,                      // ✔ طالما HTTPS
                SameSite = SameSiteMode.None       // ✔ لازم None علشان cross-site
            };

            // token is inside result.Token
            Response.Cookies.Append("jwt", result.Token, cookieOptions);

            // ==========================================================

            // ممكن تبعت جزء تاني للـ front (بدون التوكين)
            result.Token = null; // أمان: مش هنرجعه للفرونت

            return Ok(result);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,         // نفس إعدادات الكوكي الأصلي
                SameSite = SameSiteMode.None,
                Path = "/"             // مهم علشان الكوكي يتحذف صح
            });

            return Ok(new
            {
                message = "Logged out successfully"
            });
        }


        [Authorize]
        [HttpGet("check-auth")]
        public IActionResult CheckAuth()
        {
            return Ok(true);
        }

        [HttpGet("GetCurrentUser")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var IdUser= User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (IdUser == null) return BadRequest(new APIErrerResponse(400, "Invalid Email of User"));

            var CurrentUser = await userManager.FindByIdAsync(IdUser);
            if (CurrentUser == null) return BadRequest(new APIErrerResponse(400, "Invalid User"));

            return Ok(new { CurrentUser.FullName,CurrentUser.Email,CurrentUser.IsActive,CurrentUser.Id });

        }
    }
}
