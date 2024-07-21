using Domain.Entities;
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

namespace Infrastructure.JWT
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration config, UserManager<User> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigninKey"]));
            _userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            try
            {
                List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                 new Claim(JwtRegisteredClaimNames.Name, "testClaim"),

            };
               
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                    if (role == "Admin")
                    {
                        claims.Add(new Claim("AdminClaim", "true"));
                    }
                    else if (role == "User")
                    {
                        claims.Add(new Claim("UserClaim", "true"));
                    }
                }
                var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = credentials,
                    Issuer = _config["JWT:Issuer"],
                    Audience = _config["JWT:Audience"]

                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }

            
        }
    }
}
