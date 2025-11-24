using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Votes.Dtos;
using AutoMapper;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public VoteRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddVoteAsync(Vote vote, CancellationToken cancellationToken = default)
        {
            await _context.Votes.AddAsync(vote, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PollVoteDto> GetVotesByPollIdAsync(int pollId, CancellationToken cancellationToken = default)
        {
            var poll = await _context.Polls
                .Include(p => p.Votes)
                .FirstOrDefaultAsync(p => p.Id == pollId, cancellationToken);
            if (poll == null)
            {
                return null!;
            }
            return _mapper.Map<PollVoteDto>(poll);

        }

        public async Task<bool> HasUserVotedAsync(string userId, int pollId, CancellationToken cancellationToken = default)
        {
            return await _context.Votes.AnyAsync(v => v.UserId == userId && v.PollId == pollId, cancellationToken);
        }
    }
}
