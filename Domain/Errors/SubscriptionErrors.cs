using System.Net;

namespace Domain.Errors
{
    public static class SubscriptionErrors
    {
        public static Error ActiveSubscriptionExists => new("Subscription.ActiveExists", "An active subscription already exists.", HttpStatusCode.Conflict);
        public static Error NoActiveSubscription => new("Subscription.NoActive", "No active subscription found.", HttpStatusCode.NotFound);
        public static Error InvalidBillingCycle => new("Subscription.InvalidBillingCycle", "Invalid billing cycle.", HttpStatusCode.BadRequest);
        public static Error InvalidPrice => new("Subscription.InvalidPrice", "Invalid price format.", HttpStatusCode.BadRequest);
        public static Error SubscriptionNotFound => new("Subscription.NotFound", "Subscription not found.", HttpStatusCode.NotFound);
        public static Error PaymentFailed => new("Subscription.PaymentFailed", "Payment processing failed.", HttpStatusCode.PaymentRequired);
        public static Error AlreadyCancelled => new("Subscription.AlreadyCancelled", "Subscription is already cancelled.", HttpStatusCode.BadRequest);
    }
}