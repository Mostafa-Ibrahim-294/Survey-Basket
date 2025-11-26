using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Enums;
using Domain.Errors;
using MediatR;
using OneOf;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Callback;

namespace Application.Features.Subscriptions.Commands.CreatePaymentWebHook
{
    internal class CreatePaymentWebHookCommandHandler : IRequestHandler<CreatePaymentWebHookCommand, OneOf<bool, Error>>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPaymobCashInBroker _broker;
        public CreatePaymentWebHookCommandHandler(ISubscriptionRepository subscriptionRepository, IPaymobCashInBroker broker)
        {
            _subscriptionRepository = subscriptionRepository;
            _broker = broker;
        }
        public async Task<OneOf<bool, Error>> Handle(CreatePaymentWebHookCommand request, CancellationToken cancellationToken)
        {
            if (request.CallBack.Type is null || request.CallBack.Obj is null)
                return SubscriptionErrors.PaymentFailed;

            var content = ((JsonElement)request.CallBack.Obj).GetRawText();

            switch (request.CallBack.Type.ToUpperInvariant())
            {
                case CashInCallbackTypes.Transaction:
                    var transaction = JsonSerializer.Deserialize<CashInCallbackTransaction>(content)!;

                    var valid = _broker.Validate(transaction, request.Hmac);
                    if (!valid)
                        return SubscriptionErrors.PaymentFailed;

                    var merchantId = transaction.Order?.MerchantOrderId.ToString();
                    if (string.IsNullOrEmpty(merchantId))
                    {
                        return SubscriptionErrors.PaymentFailed;
                    }


                    var subscription = await _subscriptionRepository.GetSubscriptionByMerchantId(merchantId);
                    if (subscription == null)
                    {
                        return SubscriptionErrors.SubscriptionNotFound;
                    }

                    if (transaction.Success)
                    {
                        subscription.Status = SubscriptionStatus.Active;
                        subscription.ProviderTransactionId = transaction.Id.ToString();
                        subscription.RenewalDate = DateTime.Now.AddMonths(subscription.BillingCycle == BillingCycle.Monthly ? 1 : 12);
                    }

                    await _subscriptionRepository.SaveAsync();
                    return true;

                default:
                    return SubscriptionErrors.PaymentFailed;
            }
        }
    }
}
