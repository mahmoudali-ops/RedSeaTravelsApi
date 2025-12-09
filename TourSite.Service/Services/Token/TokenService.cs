using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;

namespace TourSite.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration _configuration )
        {
            configuration = _configuration;
        }
        public async Task<string> CreateTokenAsync(User user, UserManager<User> userManager)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var signinCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
           
            List<Claim> UserClasims = new List<Claim>();

            // Generate Id for GUID for token

            UserClasims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            UserClasims.Add(new Claim(ClaimTypes.Name, user.FullName ?? user.UserName ?? ""));
            UserClasims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));
            UserClasims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id ?? Guid.NewGuid().ToString())); // fallback

            var UserRoles = await userManager.GetRolesAsync(user);

            // لو مفيش Roles أصلاً
            if (UserRoles == null || UserRoles.Count == 0)
            {
                UserRoles = new List<string>(); // خليه فاضي ومتعملش Claims
            }

            foreach (var role in UserRoles)
            {
                if (!string.IsNullOrWhiteSpace(role))
                    UserClasims.Add(new Claim(ClaimTypes.Role, role));
            }

            var mytoken = new JwtSecurityToken(
                           issuer: configuration["JwtSettings:Issuer"] ,
                           audience: configuration["JwtSettings:Audience"],
                           expires: DateTime.Now.AddDays(1),
                           claims: UserClasims,
                           signingCredentials: signinCred
                       );

            return new JwtSecurityTokenHandler().WriteToken(mytoken);
        }
    }
}
