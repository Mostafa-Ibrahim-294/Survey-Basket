using System;
using Application.Features.Plans.Dtos;

namespace Application.Features.Subscriptions.Dtos
{
    public class SubscriptionDto
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string PlanId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string? ProviderId { get; set; }
        public string? ProviderPendingId { get; set; }
        public string? ProviderSubscriptionId { get; set; }
        public string? ProviderTransactionId { get; set; }
        public string BillingCycle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CanceledAt { get; set; }
        public DateTime? RenewalDate { get; set; }
        public PlanDto Plan { get; set; } = null!;
    }
}