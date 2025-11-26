using System;

namespace Application.Features.Subscriptions.Dtos
{
    public class SubscriptionDto
    {
        public PlanSummaryDto Plan { get; set; } = new();
        public string Id { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
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
    }
}