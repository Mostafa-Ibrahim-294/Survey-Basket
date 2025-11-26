using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Entites;
using Domain.Enums;
using Domain.Errors;
using MediatR;

namespace Application.Features.Subscriptions.Commands.ChangePlan
{
    internal class ChangePlanCommandHandler : IRequestHandler<ChangePlanCommand>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;
        public ChangePlanCommandHandler(ISubscriptionRepository subscriptionRepository, IUserContext userContext, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _userContext = userContext;
            _mapper = mapper;
        }
        public async Task Handle(ChangePlanCommand request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()!.UserId;
            var subscription = await _subscriptionRepository.GetActiveSubscriptionByUserId(userId, cancellationToken);
            subscription.Status = SubscriptionStatus.Canceled;
            subscription.CanceledAt = DateTime.UtcNow;
            var newSubscription = _mapper.Map<Subscription>(request);
            newSubscription.UserId = userId;
            newSubscription.Status = SubscriptionStatus.Active;
            await _subscriptionRepository.CreateSubscription(newSubscription, cancellationToken);

        }
    }
}
