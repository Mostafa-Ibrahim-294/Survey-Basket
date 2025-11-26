using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Subscription
    {

        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;

        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;

        public BillingCycle BillingCycle { get; set; }

        public SubscriptionStatus Status { get; set; }
        public Guid MerchantId { get; set; } = new Guid();

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? RenewalDate { get; set; }

        public string? ProviderId { get; set; }
        public string? ProviderTransactionId { get; set; }
    }
}
