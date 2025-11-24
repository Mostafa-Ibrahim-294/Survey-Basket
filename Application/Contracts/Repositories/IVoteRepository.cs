using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Questions.Dtos;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IVoteRepository
    {
        Task<int> GetVoteCountByAnswerIdAsync(int answerId, CancellationToken cancellationToken = default);
        Task<bool> HasUserVotedAsync(string userId, int pollId, CancellationToken cancellationToken = default);
        Task AddVoteAsync(Vote vote, CancellationToken cancellationToken = default);
    }
}
