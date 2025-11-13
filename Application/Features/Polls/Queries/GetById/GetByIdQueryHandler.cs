using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using MediatR;

namespace Application.Features.Polls.Queries.GetById
{
    internal class GetByIdQueryHandler : IRequestHandler<GetByIdQuery, PollDto?>
    {
        private readonly IPollRepository _repo;
        private readonly IMapper _mapper;

        public GetByIdQueryHandler(IPollRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PollDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id, cancellationToken);
            return entity is null ? null : _mapper.Map<PollDto>(entity);
        }
    }
}