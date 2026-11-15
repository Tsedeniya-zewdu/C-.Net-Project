
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.interfaces;
using api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _config = config;

            _key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _config["Jwt:SigningKey"]!
                )
            );
        }

        public string CreateToken(AppUser user)
        {
            // 1. Create claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Name, user.UserName!)
            };

            // 2. Create signing credentials
            var credentials = new SigningCredentials(
                _key,
                SecurityAlgorithms.HmacSha512Signature
            );

            // 3. Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),

                Expires = DateTime.UtcNow.AddDays(7),

                Issuer = _config["Jwt:Issuer"],

                Audience = _config["Jwt:Audience"],

                SigningCredentials = credentials
            };

            // 4. Generate token
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return token as string
            return tokenHandler.WriteToken(token);
        }
    }
}
