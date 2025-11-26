using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Domain.Enums;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Subscriptions.Commands.CancelSubscription
{
    internal class CancelSubscriptionCommandHandler : IRequestHandler<CancelSubscriptionCommand, OneOf<bool, Error>>
    {
        private readonly IUserContext _userContext;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public CancelSubscriptionCommandHandler(IUserContext userContext, ISubscriptionRepository subscriptionRepository)
        {
            _userContext = userContext;
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<OneOf<bool, Error>> Handle(CancelSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()!.UserId;
            var subscription = await _subscriptionRepository.GetActiveSubscriptionByUserId(userId, cancellationToken);
            if(subscription is null)
            {
                return SubscriptionErrors.AlreadyCancelled;
            }
            subscription.Status = SubscriptionStatus.Canceled;
            subscription.CanceledAt = DateTime.UtcNow;
            await _subscriptionRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
