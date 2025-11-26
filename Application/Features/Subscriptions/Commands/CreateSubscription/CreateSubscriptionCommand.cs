using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Subscriptions.Commands.CreateSubscription
{
    public sealed record CreateSubscriptionCommand(int PlanId, string Currency, decimal Price, BillingCycle BillingCycle)
        : IRequest<OneOf<string, Error>>;
}
