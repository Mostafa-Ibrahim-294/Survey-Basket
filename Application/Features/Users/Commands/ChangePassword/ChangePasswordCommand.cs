using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Users.Commands.ChangePassword
{
    public record ChangePasswordCommand(string CurrentPassword, string NewPassword) : IRequest<OneOf<bool, Error>>;

}
