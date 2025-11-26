using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Repositories;
using Application.Features.Plans.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;

namespace Application.Features.Plans.Queries.GetAllPlans
{
    internal class GetAllPlansQueryHandler : IRequestHandler<GetAllPlansQuery, IEnumerable<PlanDto>>
    {
        private readonly IPlanRepository _planRepository;
        private readonly IMapper _mapper;
        private readonly HybridCache _hybridCache;
        public GetAllPlansQueryHandler(IPlanRepository planRepository, IMapper mapper, HybridCache hybridCache)
        {
            _planRepository = planRepository;
            _mapper = mapper;
            _hybridCache = hybridCache;
        }

        public async Task<IEnumerable<PlanDto>> Handle(GetAllPlansQuery request, CancellationToken cancellationToken)
        {
            var entities = await _hybridCache.GetOrCreateAsync<IEnumerable<PlanDto>>(
                CacheConstants.AllPlans,
                async entry => {
                    var plans = await _planRepository.GetAllAsync(cancellationToken);
                    return _mapper.Map<IEnumerable<PlanDto>>(plans);
                });
            return entities;
        }
    }
}
