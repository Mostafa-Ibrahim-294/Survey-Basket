using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class PollRepository : IPollRepository
    {
        private readonly AppDbContext _context;
        public PollRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Poll> CreateAsync(Poll poll, CancellationToken cancellationToken = default)
        {
            await _context.Polls.AddAsync(poll, cancellationToken);
            await SaveAsync(cancellationToken);
            return poll;
        }

        public async Task DeleteAsync(Poll poll, CancellationToken cancellationToken = default)
        {
            _context.Polls.Remove(poll);
            await SaveAsync(cancellationToken);
        }

        public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Poll?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
           return await _context.Polls.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Poll poll, CancellationToken cancellationToken = default)
        {
            _context.Polls.Update(poll);
            await SaveAsync(cancellationToken);
        }
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
