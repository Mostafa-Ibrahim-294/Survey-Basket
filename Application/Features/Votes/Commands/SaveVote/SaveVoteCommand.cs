using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Votes.Dtos;
using MediatR;

namespace Application.Features.Votes.Commands.SaveVote
{
    public record SaveVoteCommand(int PollId, IEnumerable<VoteAnswerDto> Answers) : IRequest;

}
