using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Contracts.Repositories;
using Application.Features.Polls.Dtos;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class PollRepository : IPollRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PollRepository(AppDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<bool> IsTitleExist(string title, CancellationToken cancellationToken = default)
        {
            return await _context.Polls.AnyAsync(p => p.Title == title, cancellationToken);
        }

        public async Task<bool> IsTitleExistForOtherId(string title, int id, CancellationToken cancellationToken = default)
        {
            return await _context.Polls.AnyAsync(p => p.Title == title && p.Id != id, cancellationToken);
        }

        public async Task<IEnumerable<Poll>> GetCurrentPolls(CancellationToken cancellationToken = default)
        {
            return await _context.Polls
                .Where(p => p.IsPublished && p.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow)
                && p.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow)
                ).AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public Task<bool> IsCurrentPoll(int id, CancellationToken cancellationToken = default)
        {
            return _context.Polls.AnyAsync(p => p.Id == id && p.IsPublished && p.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow)
                && p.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
        }

        public async Task<PageResult<PollDto>> GetAllAsync(int pageNumber, int pageSize, string? search,
            string? sortBy = null, SortDirection sortDirection = SortDirection.Ascending,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Polls.AsNoTracking()
                .Where(p => string.IsNullOrEmpty(search) || p.Title.Contains(search))
                .ProjectTo<PollDto>(_mapper.ConfigurationProvider);
            if (!string.IsNullOrEmpty(sortBy))
                query = sortDirection == SortDirection.Ascending
                ? query.OrderBy(p => p.Title)
                : query.OrderByDescending(p => p.Title);
            return await PageResult<PollDto>.CreateAsync(query, pageNumber, pageSize);
        }
    }
}
