using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Users.Dtos;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Queries.GetAllUsers
{
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserResponse>>
    {
        private readonly IUserRepository userRepository;
        
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<IEnumerable<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await userRepository.GetAllWithRolesAsync(cancellationToken);
            return result;
        }
    }
}
