using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Users.Commands.Register
{
    public record RegisterCommand(string FirstName, string LastName, string Email, string Password) : IRequest<OneOf<bool, Error>>;
}
