using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Votes.Dtos;
using MediatR;

namespace Application.Features.Votes.Queries.GetVotesByPoll
{
    internal class GetVotesByPollQueryHandler : IRequestHandler<GetVotesByPollQuery,PollVoteDto>
    {
        private readonly IVoteRepository _voteRepository;
        public GetVotesByPollQueryHandler(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }
        public async Task<PollVoteDto> Handle(GetVotesByPollQuery request, CancellationToken cancellationToken)
        {
            return await _voteRepository.GetVotesByPollIdAsync(request.PollId, cancellationToken);
        }
    }
}
