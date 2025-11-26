using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface ISubscriptionRepository
    {
        Task CreateSubscription(Subscription subscription, CancellationToken cancellationToken = default);
        Task<Subscription?> GetSubscriptionByMerchantId(string merchantId, CancellationToken cancellationToken = default);
        Task<Subscription?> GetActiveSubscriptionByUserId(string userId, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
