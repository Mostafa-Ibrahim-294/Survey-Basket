using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Commands.Create;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Roles.Dtos
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
