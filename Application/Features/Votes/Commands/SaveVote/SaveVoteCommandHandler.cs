using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Features.Votes.Commands.SaveVote
{
    internal class SaveVoteCommandHandler : IRequestHandler<SaveVoteCommand>
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        public SaveVoteCommandHandler(IVoteRepository voteRepository, IUserContext userContext , IMapper mapper)
        {
            _voteRepository = voteRepository;
            _userContext = userContext;
            _mapper = mapper;

        }

        public async Task Handle(SaveVoteCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()!.UserId;
            
            var vote = _mapper.Map<Vote>(request);
            vote.UserId = userId;
            await _voteRepository.AddVoteAsync(vote, cancellationToken);
        }
    }
}
