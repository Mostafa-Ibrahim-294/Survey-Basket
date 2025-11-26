using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using MediatR;

namespace Application.Features.Subscriptions.Commands.ChangePlan
{
    public sealed record ChangePlanCommand(int PlanId, BillingCycle BillingCycle, decimal Price, string Currency) : IRequest;
}
