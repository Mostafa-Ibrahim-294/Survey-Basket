using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Commands.Register;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Users.Dtos
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommand , ApplicationUser>()
                .ForMember(dest => dest.UserName , opt => opt.MapFrom(src => src.Email));
        }
    }
}
