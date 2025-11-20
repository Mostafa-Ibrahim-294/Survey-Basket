using System.Collections.Generic;
using Domain.Errors;
using Application.Features.Users.Dtos;
using MediatR;
using OneOf;

namespace Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        IEnumerable<string> Roles
    ) : IRequest<OneOf<UserResponse, Error>>;
}