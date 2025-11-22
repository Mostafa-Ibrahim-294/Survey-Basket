using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Questions.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Commands.CreateQuestion
{
    public record CreateQuestionCommand(string Content , IEnumerable<string> Answers) : IRequest<OneOf<QuestionDto,Error>>;
}
