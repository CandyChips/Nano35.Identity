using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Nano35.Identity.Api.Services.Helpers
{
    public class AuthOptions
    {
        const string KEY = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}