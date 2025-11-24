using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;
        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddVoteAsync(Vote vote, CancellationToken cancellationToken = default)
        {
            await _context.Votes.AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public Task<int> GetVoteCountByAnswerIdAsync(int answerId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> HasUserVotedAsync(string userId, int pollId, CancellationToken cancellationToken = default)
        {
            return await _context.Votes.AnyAsync(v => v.UserId == userId && v.PollId == pollId, cancellationToken);
        }
    }
}
