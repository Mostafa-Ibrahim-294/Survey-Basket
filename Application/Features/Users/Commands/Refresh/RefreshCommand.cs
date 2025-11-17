using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;
namespace Application.Features.Users.Commands.Refresh
{
    public record RefreshCommand(string RefreshToken) : IRequest<OneOf<AuthResponse, Error>>;
}
