using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Users.Commands.Register
{
    public record RegisterCommand(string FirstName , string LastName , string Email , string Password) : IRequest<bool>;
}
