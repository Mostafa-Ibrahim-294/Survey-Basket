using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Users.Commands.ConfirmEmail
{
    public record ConfirmEmailCommand(string UserId, string Code) : IRequest<OneOf<bool, Error>>;
}
