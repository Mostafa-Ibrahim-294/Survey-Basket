using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Features.Users.Commands.ResendConfirmationEmail
{
    public record ResendConfirmationEmailCommand(string Email) : IRequest<bool>;
}
