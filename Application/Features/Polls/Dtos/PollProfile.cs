using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Polls.Commands.Create;
using Application.Features.Polls.Commands.Update;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Polls.Dtos
{
    public class PollProfile : Profile
    {
        public PollProfile()
        {
            CreateMap<Poll, PollDto>();
            CreateMap<CreateCommand, Poll>();
            CreateMap<UpdateCommand, Poll>();
        }
    }
}
