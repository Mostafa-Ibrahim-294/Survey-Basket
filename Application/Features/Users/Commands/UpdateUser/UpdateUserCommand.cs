using System.Collections.Generic;
using Domain.Errors;
using Application.Features.Users.Dtos;
using MediatR;
using OneOf;

namespace Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(
        string Id,
        string FirstName,
        string LastName,
        string Email,
        IEnumerable<string> Roles,
        bool IsDisabled
    ) : IRequest<OneOf<UserResponse, Error>>;
}