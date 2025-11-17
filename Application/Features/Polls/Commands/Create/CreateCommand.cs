using Application.Features.Polls.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Polls.Commands.Create
{
    public record CreateCommand : IRequest<OneOf<PollDto, Error>>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }
    }
}