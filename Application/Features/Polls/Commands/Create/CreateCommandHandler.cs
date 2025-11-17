using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Polls.Commands.Create
{
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, OneOf<PollDto, Error>>
    {
        private readonly IPollRepository _repo;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IPollRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OneOf<PollDto, Error>> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repo.IsTitleExist(request.Title, cancellationToken);
            if (exists)
            {
                return PollErrors.DuplicateTitle;
            }
            var toCreate = _mapper.Map<Poll>(request);
            var created = await _repo.CreateAsync(toCreate, cancellationToken);
            return _mapper.Map<PollDto>(created);
        }
    }
}