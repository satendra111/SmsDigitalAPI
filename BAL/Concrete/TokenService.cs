using BAL.Abstract;
using Domain.CommonEntity;
using Domain.EntityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SelfOnBoarding.Bll.Concrete
{
    public class TokenService : ITokenService
    {
        private readonly AppSettings appSettings;
      
        public TokenService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;  
        }
    

        public string GenerateToken(UserEntityModel userEntityModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userEntityModel.UserId.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userEntityModel.Email),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(appSettings.ExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = appSettings.Issuer,
                Audience = appSettings.Audience,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}
