using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Models;

namespace Nano35.Identity.Processor.Models
{
    public class Role : IdentityRole
    {
        public Role(string Name): base(Name)
        {
            
        }
    }

    public class RolesAutoMapperProfile : Profile
    {
        public RolesAutoMapperProfile()
        {
            CreateMap<Role, IRoleViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name));
        }
    }
}