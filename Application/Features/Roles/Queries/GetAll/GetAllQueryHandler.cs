using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Roles.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Roles.Queries.GetAll
{
    internal class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<RoleDto>>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public GetAllQueryHandler(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RoleDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);
            var rolesDto = _mapper.Map<List<RoleDto>>(roles);
            return rolesDto;
        }
    }
}
