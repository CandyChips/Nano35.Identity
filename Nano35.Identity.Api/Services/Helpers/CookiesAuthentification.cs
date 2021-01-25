using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Nano35.Identity.Api.Services.Helpers
{
    public class CookiesAuthStateProvider : 
        ICustomAuthStateProvider
    {
        private Guid WorkerId { get; set;}

        public Guid CurrentUserId => this.WorkerId;

        public CookiesAuthStateProvider(
            IHttpContextAccessor httpContextAccessor)
        {
            var jwtEncoded = httpContextAccessor.HttpContext.Request.Headers["Authorization"]!.ToString().Split(' ').Last();
            if (jwtEncoded == null) return;
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(jwtEncoded);
            this.WorkerId = Guid.Parse(jwt.Claims.First().Value);
        }
    }
    public interface ICustomAuthStateProvider
    {
        Guid CurrentUserId {get;}
    }
}