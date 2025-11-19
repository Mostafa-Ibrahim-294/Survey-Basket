using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Roles.Dtos;
using AutoMapper;
using Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OneOf;

namespace Application.Features.Roles.Commands.Update
{
    internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, OneOf<RoleDto, Error>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UpdateCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<OneOf<RoleDto, Error>> Handle(UpdateCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null)
            {
                return RoleErrors.RoleNotFound;
            }
            var isExistingRole = await _roleManager.Roles.AnyAsync(r => r.Name == request.Name && r.Id != request.Id, cancellationToken);
            if (isExistingRole)
            {
                return RoleErrors.RoleAlreadyExists;
            }
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
            {
                return RoleErrors.RoleCreationFailed;
            }
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;

        }
    }
}
