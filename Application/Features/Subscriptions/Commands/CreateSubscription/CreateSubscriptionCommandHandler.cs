using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Payment;
using Application.Contracts.Repositories;
using AutoMapper;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using OneOf;
using Domain.Enums;
using Application.Contracts.Authentication;

namespace Application.Features.Subscriptions.Commands.CreateSubscription
{
    internal class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, OneOf<string, Error>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        private readonly IUserContext _userContext;


        public CreateSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IPlanRepository planRepository,
            IPaymentService paymentService, IMapper mapper, IUserContext userContext)
        {
            _subscriptionRepository = subscriptionRepository;
            _planRepository = planRepository;
            _paymentService = paymentService;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<OneOf<string, Error>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var plan = await _planRepository.GetByIdAsync(request.PlanId, cancellationToken);
            if (plan is null)
            {
               return PlanErrors.PlanNotFound;
            }
            var subscription = _mapper.Map<Subscription>(request);
            subscription.Status = SubscriptionStatus.Pending;
            subscription.UserId = _userContext.GetCurrentUser()!.UserId;
            return await _paymentService.CreatePaymentSessionAsync(subscription);
        }
    }
}
