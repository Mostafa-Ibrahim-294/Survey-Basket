using System.Linq;
using Application.Features.Votes.Commands.SaveVote;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Votes.Dtos
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            // Command -> Entity (build VoteAnswers collection)
            CreateMap<SaveVoteCommand, Vote>()
                .ForMember(dest => dest.VoteAnswers, opt => opt.MapFrom(src =>
                    src.Answers.Select(a => new VoteAnswer
                    {
                        QuestionId = a.QuestionId,
                        AnswerId = a.AnswerId
                    })));

            CreateMap<Poll, PollVoteDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes));

            CreateMap<Vote, VoteDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.VoteDate, opt => opt.MapFrom(src => src.SubmittedOn))
                .ForMember(dest => dest.QuestionAnswers, opt => opt.MapFrom(src => src.VoteAnswers));

            CreateMap<VoteAnswer, QuestionAnswerResponse>()
                .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question.Content))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Answer.Content));
        }
    }
}
