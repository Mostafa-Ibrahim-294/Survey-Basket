using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Roles.Dtos;
using MediatR;

namespace Application.Features.Roles.Queries.GetAll
{
    public record GetAllQuery : IRequest<IEnumerable<RoleDto>>;
}
