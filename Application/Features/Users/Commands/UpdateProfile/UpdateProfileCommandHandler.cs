using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using AutoMapper;
using Domain.Entites;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.Commands.UpdateProfile
{
    internal class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;
        public UpdateProfileCommandHandler(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IUserContext userContext)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userContext = userContext;
        }
        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()!.UserId;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            _mapper.Map(request, user);
            await _userManager.UpdateAsync(user!);
        }
    }
}
