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
using OneOf;

namespace Application.Features.Roles.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, OneOf<RoleDto, Error>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public CreateCommandHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<OneOf<RoleDto, Error>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var roleExists = await _roleManager.RoleExistsAsync(request.Name);
            if (roleExists)
            {
                return RoleErrors.RoleAlreadyExists;
            }
            var role = new IdentityRole
            {
                Name = request.Name ,
                NormalizedName = request.Name.ToUpperInvariant()
            };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return RoleErrors.RoleCreationFailed;
            }
            var roleDto = _mapper.Map<RoleDto>(role);
            return roleDto;

        }
    }
}
