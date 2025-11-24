using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Votes.Commands.SaveVote;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Votes.Dtos
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<SaveVoteCommand, Vote>()
                .ForMember(dest => dest.VoteAnswers, opt => opt.MapFrom(src => src.Answers.Select(a => new VoteAnswer
                {
                    QuestionId = a.QuestionId,
                    AnswerId = a.AnswerId
                })));
        }
    }
}
