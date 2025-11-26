namespace Application.Features.Subscriptions.Dtos
{
    public class PlanSummaryDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}