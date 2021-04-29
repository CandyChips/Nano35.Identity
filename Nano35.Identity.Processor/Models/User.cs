using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
}