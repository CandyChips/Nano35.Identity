using Microsoft.AspNetCore.Identity;

namespace Nano35.Identity.Processor.Models
{
    public class Role : IdentityRole
    {
        public Role(string name): base(name)
        {
            
        }
    }
}