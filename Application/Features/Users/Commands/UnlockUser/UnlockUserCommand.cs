using Domain.Errors;
using Application.Features.Users.Dtos;
using MediatR;
using OneOf;

namespace Application.Features.Users.Commands.UnlockUser
{
    public record UnlockUserCommand(string Id) : IRequest<OneOf<bool, Error>>;
}