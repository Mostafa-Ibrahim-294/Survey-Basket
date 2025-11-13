using Application.Features.Polls.Dtos;
using MediatR;

namespace Application.Features.Polls.Commands.Create
{
    public record CreateCommand : IRequest<PollDto>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsPublished { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }
    }
}