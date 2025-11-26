using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class Invoice
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public string PlanName { get; set; } = string.Empty;

        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;

        public DateTime PaidAt { get; set; }
        public BillingCycle BillingCycle { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
