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

namespace Application.Features.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, OneOf<UserResponse, Error>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<OneOf<UserResponse, Error>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _userManager.FindByEmailAsync(request.Email);
            if (existing != null)
                return UserErrors.EmailAlreadyExists;
            var rolesList = request.Roles.Distinct().ToList();
            var missing = rolesList.Where(r => !_roleManager.Roles.Any(rr => rr.Name == r)).ToList();
            if (missing.Any())
                return UserErrors.RoleNotFound;

            var user = _mapper.Map<ApplicationUser>(request);
            user.EmailConfirmed = true; 
            var createResult = await _userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
                return UserErrors.FailedToCreate;
                var rolesResult = await _userManager.AddToRolesAsync(user, rolesList);
                if (!rolesResult.Succeeded)
                    return UserErrors.FailedToCreate;
            var response = _mapper.Map<UserResponse>(user);
            response.Roles = rolesList;
            return response;
        }
    }
}