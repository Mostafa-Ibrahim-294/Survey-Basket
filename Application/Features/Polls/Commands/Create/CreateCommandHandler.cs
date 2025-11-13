using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Features.Polls.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, PollDto>
    {
        private readonly IPollRepository _repo;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IPollRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PollDto> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            // Ignore incoming Id if any during creation
            var toCreate = _mapper.Map<Poll>(request);
            var created = await _repo.CreateAsync(toCreate, cancellationToken);
            return _mapper.Map<PollDto>(created);
        }
    }
}