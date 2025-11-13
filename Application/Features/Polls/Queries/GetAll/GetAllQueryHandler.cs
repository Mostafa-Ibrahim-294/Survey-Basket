using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Polls.Queries.GetAll
{
    internal class GetAllQueryHandler : IRequestHandler<GetAllQuery, IEnumerable<PollDto>>
    {
        private readonly IPollRepository _repo;
        private readonly IMapper _mapper;

        public GetAllQueryHandler(IPollRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PollDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PollDto>>(entities);
        }
    }
}
