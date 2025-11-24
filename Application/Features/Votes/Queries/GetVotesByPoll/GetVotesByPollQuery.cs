using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Votes.Dtos;
using MediatR;

namespace Application.Features.Votes.Queries.GetVotesByPoll
{
    public record class GetVotesByPollQuery(int PollId) : IRequest<PollVoteDto>;
}
