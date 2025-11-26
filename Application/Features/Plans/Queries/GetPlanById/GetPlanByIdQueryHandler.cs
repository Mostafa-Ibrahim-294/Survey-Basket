using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Plans.Dtos;
using AutoMapper;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Plans.Queries.GetPlanById
{
    internal class GetPlanByIdQueryHandler : IRequestHandler<GetPlanByIdQuery, OneOf<PlanDto, Error>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        public GetPlanByIdQueryHandler(IPlanRepository planRepository, IMapper mapper)
        {
            _planRepository = planRepository;
            _mapper = mapper;
        }
        public async Task<OneOf<PlanDto, Error>> Handle(GetPlanByIdQuery request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(request.Id, cancellationToken);
            if (plan == null)
            {
                return PlanErrors.PlanNotFound;
            }
            return _mapper.Map<PlanDto>(plan);
        }
    }
}
