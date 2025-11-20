using System.Threading;
using System.Threading.Tasks;
using Domain.Entites;
using Domain.Errors;
using Application.Features.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using System.Linq;

namespace Application.Features.Users.Commands.UnlockUser
{
    internal class UnlockUserCommandHandler : IRequestHandler<UnlockUserCommand, OneOf<bool, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UnlockUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<OneOf<bool, Error>> Handle(UnlockUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
                return UserErrors.UserNotFound;

            if (user.LockoutEnd == null || user.LockoutEnd <= DateTimeOffset.UtcNow)
                return UserErrors.UserNotLocked;

            var resetFailed = await _userManager.ResetAccessFailedCountAsync(user);
            var unlockResult = await _userManager.SetLockoutEndDateAsync(user, null);
            if (!resetFailed.Succeeded || !unlockResult.Succeeded)
                return UserErrors.FailedToUnlock;
            return true;
        }
    }
}