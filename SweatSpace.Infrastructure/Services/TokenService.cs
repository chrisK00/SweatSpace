using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SweatSpace.Core.Entities;
using SweatSpace.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SweatSpace.Infrastructure.Services
{
    internal class TokenService : ITokenService
    {
        //key will never leave our server
        private readonly SymmetricSecurityKey _key;

        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            //get a string of text using its key then convert it into a byte array
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(AppUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            //select - map. Map role name to a new claim's name
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            //validation credentials, algorithm to use for securing the creds
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //building the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}