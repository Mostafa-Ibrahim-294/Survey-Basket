using Application.Features.Questions.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Commands.ToggleQuestion
{
    public record ToggleQuestionCommand(int PollId, int Id) : IRequest<OneOf<bool, Error>>;
}