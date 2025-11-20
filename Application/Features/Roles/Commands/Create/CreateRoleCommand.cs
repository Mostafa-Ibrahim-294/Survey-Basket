using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Roles.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Roles.Commands.Create
{
    public record CreateRoleCommand(string Name) : IRequest<OneOf<RoleDto, Error>>;
}
