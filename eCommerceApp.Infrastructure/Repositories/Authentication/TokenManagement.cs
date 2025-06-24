﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using eCommerceApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eCommerceApp.Infrastructure.Repositories.Authentication {
    public partial class RoleManagement {
        public class TokenManagement(AppDbContext context, IConfiguration config) : ITokenManagement {
            public async Task<int> AddRefreshToken(string userId, string refreshToken) {
                context.RefreshTokens.Add(new RefreshToken {
                    UserId = userId,
                    Token = refreshToken,
                });
                return await context.SaveChangesAsync();
            }

            public string GenerateToken(List<Claim> claims) {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiration = DateTime.UtcNow.AddHours(2);
                var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    audience: config["Jwt:Audience"],
                    claims: claims,
                    expires: expiration,
                    signingCredentials: cred
                );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            public string GetRefreshToken() {
                const int byteSize = 64;
                byte[] randomBytes = new byte[byteSize];
                using(RandomNumberGenerator rng = RandomNumberGenerator.Create()) {
                    rng.GetBytes(randomBytes);
                }
                return Convert.ToBase64String(randomBytes);
            }


            public List<Claim> GetUserClaimsFromToken(string token) {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadJwtToken(token);
                if (jwtToken != null) {
                    return jwtToken.Claims.ToList();
                }
                else {
                    return [];
                }
            }

            public async Task<string> GetUserIdByRefreshToken(string refreshToken) => (await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken))!.UserId;

            public async Task<int> UpdateRefreshToken(string userId, string refreshToken) {
                var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
                if (user == null) return -1;
                user.Token = refreshToken;
                return await context.SaveChangesAsync();
            }

            public async Task<bool> ValidateRefreshToken(string refreshToken) {
                var user = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.Token == refreshToken);
                return user != null;
            }
        }
    }
}
