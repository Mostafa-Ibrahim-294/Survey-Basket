using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entites;
using Domain.Errors;
using Application.Features.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using AutoMapper;

namespace Application.Features.Users.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, OneOf<UserResponse, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<OneOf<UserResponse, Error>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
                return UserErrors.UserNotFound;

            if (!string.Equals(user.Email, request.Email, System.StringComparison.OrdinalIgnoreCase))
            {
                var existingByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (existingByEmail != null && existingByEmail.Id != user.Id)
                    return UserErrors.EmailAlreadyExists;
            }

            var rolesList = request.Roles.Distinct().ToList();
            var missing = rolesList.Where(r => !_roleManager.Roles.Any(rr => rr.Name == r)).ToList();
            if (missing.Any())
                return UserErrors.RoleNotFound;

            user = _mapper.Map(request, user);

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return UserErrors.FailedToUpdate;

            var currentRoles = await _userManager.GetRolesAsync(user);
            var toRemove = currentRoles.Except(rolesList).ToList();
            var toAdd = rolesList.Except(currentRoles).ToList();

            if (toRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, toRemove);
                if (!removeResult.Succeeded)
                    return UserErrors.FailedToUpdate;
            }

            if (toAdd.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, toAdd);
                if (!addResult.Succeeded)
                    return UserErrors.FailedToUpdate;
            }

            var finalRoles = await _userManager.GetRolesAsync(user);

            var response = _mapper.Map<UserResponse>(user);
            response.Roles = finalRoles;
            response.IsDisabled = user.IsDisabled;

            return response;
        }
    }
}