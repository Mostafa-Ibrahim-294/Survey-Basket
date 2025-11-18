using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Features.Users.Dtos;
using AutoMapper;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;

namespace Application.Features.Users.Queries.GetUserProfile
{
    internal class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery,UserDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        public GetUserProfileQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper, IUserContext userContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task<UserDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()?.UserId;
            var user = await _userManager.FindByIdAsync(userId!);
            return _mapper.Map<UserDto>(user);
        }
    }
}
