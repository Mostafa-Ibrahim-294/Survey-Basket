using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Constants;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;

namespace Application.Features.Polls.Queries.GetAll
{
    internal class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<PollDto>>
    {
        private readonly IPollRepository _repo;
        private readonly IMapper _mapper;
        private readonly HybridCache _hybridCache;
        public GetAllQueryHandler(IPollRepository repo, IMapper mapper, HybridCache hybridCache)
        {
            _repo = repo;
            _mapper = mapper;
            _hybridCache = hybridCache;
        }

        public async Task<IEnumerable<PollDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var entities = await _hybridCache.GetOrCreateAsync<IEnumerable<PollDto>>(CacheConstants.AllPolls, async entry =>
            {
                var entities = await _repo.GetAllAsync(cancellationToken);
                return _mapper.Map<IEnumerable<PollDto>>(entities);
            });

            return entities;
        }
    }
}
