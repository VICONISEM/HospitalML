﻿using Hospital.DAL.Entities;
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

namespace Hospital.BLL.TokenServices
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration configuration;

        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(ApplicationUser User, UserManager<ApplicationUser> userManager)
        {
            //Payload
            //1. Private Claims

            var AuthClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, User.UserName),
                new Claim(ClaimTypes.Email, User.Email)
            };

            var userRoles = await userManager.GetRolesAsync(User);

            foreach (var role in userRoles)
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));

            // Our Encryption Key 
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? string.Empty));

            var Token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
                claims: AuthClaims,
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
