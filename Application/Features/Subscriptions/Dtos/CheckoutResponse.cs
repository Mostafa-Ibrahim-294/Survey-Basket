namespace Application.Features.Subscriptions.Dtos
{
    public class CheckoutResponse
    {
        public string CheckoutUrl { get; set; } = string.Empty;
        public int Status { get; set; } = 201;
    }
}