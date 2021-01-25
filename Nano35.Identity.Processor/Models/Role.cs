using Microsoft.AspNetCore.Identity;

namespace Nano35.Identity.Consumers.Models
{
    public class Role : IdentityRole
    {
        public Role(string Name): base(Name)
        {
            
        }
    }
}