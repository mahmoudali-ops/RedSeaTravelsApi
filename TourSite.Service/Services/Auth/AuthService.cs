using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.User;
using TourSite.Core.DTOs.UserAuth;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;
        public AuthService(UserManager<User> _userManager
            , SignInManager<User> _signInManager
            , ITokenService _tokenService
            )
        {
            userManager = _userManager;
            TokenService = _tokenService;
            signInManager = _signInManager;
        }

        public ITokenService TokenService { get; }

       

        public async Task<UserAuthDto> LoginAsync(LoginAuthDto loginAuthDto)
        {
            var User = await userManager.FindByEmailAsync(loginAuthDto.Email);
            if (User == null) return null;
            var result = await signInManager.CheckPasswordSignInAsync(User, loginAuthDto.Password, false);
            if (!result.Succeeded) return null;
            var token = await TokenService.CreateTokenAsync(User, userManager);
            return new UserAuthDto
            {
                FullName = User.FullName,
                Email = User.Email,
                Token = token // ✅ أهم سطر

            };
        }

        public async Task<UserAuthDto> RegisterAsync(RegisterAuthDto registerAuthDto)
        {
            try
            {
                if (await CheckEmail(registerAuthDto.Email)) return null;

                User user = new User();
                user.Email = registerAuthDto.Email;
                user.UserName = registerAuthDto.Email;
                user.FullName = registerAuthDto.FullName;

                IdentityResult result = await userManager.CreateAsync(user, registerAuthDto.Password);

                if (!result.Succeeded) return null;
                var token = await TokenService.CreateTokenAsync(user, userManager);
                return new UserAuthDto
                {
                    Token = token,
                    FullName = user.FullName,

                    Email = user.Email
                };
            }
            catch (Exception ex) {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
        public async Task<bool> CheckEmail(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
