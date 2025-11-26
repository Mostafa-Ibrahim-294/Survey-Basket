using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Payment;
using Application.Contracts.Repositories;
using Application.Features.Subscriptions.Commands.CreateSubscription;
using Application.Features.Users.Dtos;
using Domain.Entites;
using Infrastructure.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Orders;
using X.Paymob.CashIn.Models.Payment;

namespace Infrastructure.Services
{
    internal class PaymentService : IPaymentService
    {
        private readonly IPaymobCashInBroker _broker;
        private readonly IOptions<PaymobOptions> _options;
        private readonly IUserContext _userContext;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public PaymentService(IPaymobCashInBroker broker, IOptions<PaymobOptions> options,
            IUserContext userContext, ISubscriptionRepository subscriptionRepository)
        {
            _broker = broker;
            _options = options;
            _userContext = userContext;
            _subscriptionRepository = subscriptionRepository;
        }
        public async Task<string> CreatePaymentSessionAsync(Subscription subscription)
        {
            var user = _userContext.GetCurrentUser();
            var amountCents = (int)(subscription.Price * 100);
            var paymentRequest = CashInCreateOrderRequest.CreateOrder(amountCents, subscription.Currency);
            var orderResponse = await _broker.CreateOrderAsync(paymentRequest);
            subscription.ProviderId = orderResponse.Id.ToString();
            await _subscriptionRepository.SaveAsync();
            CurrentUser userDto = _userContext.GetCurrentUser()!;
            var billingData = new CashInBillingData
                (
                    firstName: userDto.FirstName,
                    lastName: userDto.LastName,
                    email: userDto.Email,
                    phoneNumber: "0000000000"
                );
            var paymentKeyRequest = new CashInPaymentKeyRequest
                (
                    integrationId: _options.Value.IntegrationId,
                    orderId: orderResponse.Id,
                    billingData: billingData,
                    amountCents: amountCents
                );
            var paymentKeyResponse = await _broker.RequestPaymentKeyAsync(paymentKeyRequest);
            return _broker.CreateIframeSrc(_options.Value.IframeId.ToString(), paymentKeyResponse.PaymentKey);
        }
    }
}
