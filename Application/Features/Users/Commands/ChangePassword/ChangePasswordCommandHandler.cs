using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Users.Commands.ChangePassword;
using MediatR;
using OneOf;
using Domain.Errors;
using Microsoft.AspNetCore.Identity;
using Domain.Entites;
using Application.Contracts.Authentication;

namespace Application.Features.Users.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserContext _userContext;
        public ChangePasswordCommandHandler(UserManager<ApplicationUser> userManager, IUserContext userContext)
        {
            _userManager = userManager;
            _userContext = userContext;
        }
        public async Task<OneOf<bool, Error>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_userContext.GetCurrentUser()?.UserId!);
            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return true;
            }
            return UserErrors.FailedChangePassword;
        }
    }
}
