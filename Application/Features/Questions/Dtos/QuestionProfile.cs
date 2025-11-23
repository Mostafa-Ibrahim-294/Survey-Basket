using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Questions.Commands.CreateQuestion;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Questions.Dtos
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers.Select(a => new AnswerDto { Id = a.Id, Content = a.Content })));
            CreateMap<CreateQuestionCommand, Question>()
                .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.Answers.Select(a => new Answer { Content = a })));
            //CreateMap<UpdateQuestionDto, Domain.Entites.Question>();
        }
    }
}
