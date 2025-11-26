using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Subscriptions.Commands.CancelSubscription
{
    public sealed record CancelSubscriptionCommand : IRequest<OneOf<bool, Error>>;
}
