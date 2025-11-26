using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using FluentValidation;

namespace Application.Features.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandValidator : AbstractValidator<CreateSubscriptionCommand>
    {
        public CreateSubscriptionCommandValidator()
        {
       
            RuleFor(x => x.PlanId)
                .NotEmpty().WithMessage("PlanId is required.");
            RuleFor(x => x.BillingCycle)
                .Must(x => x == BillingCycle.Monthly || x == BillingCycle.Yearly)
                .WithMessage("BillingCycle must be either Monthly or Yearly.");

        }

    }
}
