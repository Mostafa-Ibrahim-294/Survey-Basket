using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Users.Queries.GetUser
{
    public record GetUserQuery(string id) : IRequest<OneOf<UserResponse, Error>>;
    
}
