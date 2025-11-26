using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Entites;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDbContext _context;
        public SubscriptionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateSubscription(Subscription subscription, CancellationToken cancellationToken = default)
        {
            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
            await SaveAsync(cancellationToken);
        }

        public async Task<Subscription?> GetActiveSubscriptionByUserId(string userId, CancellationToken cancellationToken = default)
        {
            return await _context.Subscriptions.AsNoTracking()
                .Include(x => x.Plan)
                .Where(s => s.UserId == userId && s.Status == SubscriptionStatus.Active)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Subscription?> GetSubscriptionByMerchantId(string merchantId, CancellationToken cancellationToken = default)
        {
            return await _context.Subscriptions.FirstOrDefaultAsync(s => s.MerchantId.ToString() == merchantId, cancellationToken);
        }

        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
