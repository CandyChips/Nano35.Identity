using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Models;

namespace Nano35.Identity.Processor.Models
{
    public class Role : IdentityRole
    {
        public Role(string name): base(name)
        {
            
        }
    }
}