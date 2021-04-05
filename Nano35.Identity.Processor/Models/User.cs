using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Nano35.Contracts.Identity.Models;

namespace Nano35.Identity.Processor.Models
{
    public class User : IdentityUser
    {
        [Required]
        [Column(TypeName="nvarchar(MAX)")]
        public string Name { get; set; }
        [Required]
        public bool Deleted { get; set; }
        
    }

    public class UsersAutoMapperProfile : Profile
    {
        public UsersAutoMapperProfile()
        {
            CreateMap<User, IUserViewModel>()
                .ForMember(dest => dest.Id, source => source
                    .MapFrom(source => source.Id))
                .ForMember(dest => dest.Name, source => source
                    .MapFrom(source => source.Name))
                .ForMember(dest => dest.Email, source => source
                    .MapFrom(source => source.Email))
                .ForMember(dest => dest.Phone, source => source
                    .MapFrom(source => source.UserName));
        }
    }
}