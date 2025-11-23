using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Questions.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Queries.GetQuestionById
{
    public record GetQuestionByIdQuery(int PollId, int Id) : IRequest<OneOf<QuestionDto, Error>>;
}
