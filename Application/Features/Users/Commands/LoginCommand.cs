using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using MediatR;

namespace Application.Features.Users.Commands
{
    public record LoginCommand(string Email, string Password) : IRequest<AuthResponse?>;

}
