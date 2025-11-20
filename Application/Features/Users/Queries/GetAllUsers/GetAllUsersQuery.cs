using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using MediatR;

namespace Application.Features.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>;
}
