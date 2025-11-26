using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Application.Features.Subscriptions.Dtos;
using AutoMapper;
using Domain.Errors;
using MediatR;
using Microsoft.Extensions.Caching.Hybrid;
using OneOf;

namespace Application.Features.Subscriptions.Queries.GetCurrentSubscription
{
    internal class GetCurrentSubscriptionQueryHandler : IRequestHandler<GetCurrentSubscriptionQuery, OneOf<SubscriptionDto, Error>>
    {
        private readonly IUserContext _userContext;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;
        private readonly HybridCache _cache;

        public GetCurrentSubscriptionQueryHandler(
            IUserContext userContext,
            ISubscriptionRepository subscriptionRepository,
            IMapper mapper,
            HybridCache cache)
        {
            _userContext = userContext;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<OneOf<SubscriptionDto, Error>> Handle(GetCurrentSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var current = _userContext.GetCurrentUser();
            if (current is null) return UserErrors.InvalidCredentials;

            var cacheKey = $"subscription_current_{current.UserId}";
            var dto = await _cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                var sub = await _subscriptionRepository.GetActiveSubscriptionByUserId(current.UserId, cancellationToken);
                if (sub is null) return null;
                return _mapper.Map<SubscriptionDto>(sub);
            },
            options : new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromHours(1)
            },
            cancellationToken: cancellationToken);


            if (dto is null) return SubscriptionErrors.NoActiveSubscription;

            return dto;
        }
    }
}