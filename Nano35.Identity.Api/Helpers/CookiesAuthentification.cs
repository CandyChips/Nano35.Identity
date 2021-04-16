using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Nano35.Identity.Api.Helpers
{
    public class AuthOptions
    {
        public const string Issuer = "Nano35.Identity.Api";
        const string Key = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
    
    public class CookiesAuthStateProvider : 
        ICustomAuthStateProvider
    {
        private Guid WorkerId { get; set;}

        public Guid CurrentUserId => WorkerId;

        public CookiesAuthStateProvider(IHttpContextAccessor httpContextAccessor)
        {
            var jwtEncoded = httpContextAccessor.HttpContext.Request.Headers["authorization"]!.ToString().Split(' ').Last();
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtEncoded);
            WorkerId = Guid.Parse(jwt.Claims.First().Value);
        }
    }
    public interface ICustomAuthStateProvider
    {
        Guid CurrentUserId {get;}
    }
}