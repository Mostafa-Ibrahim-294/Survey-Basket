using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Errors;
using MediatR;
using OneOf;
using X.Paymob.CashIn.Models.Callback;


namespace Application.Features.Subscriptions.Commands.CreatePaymentWebHook
{
    public record CreatePaymentWebHookCommand(string Hmac, CashInCallback CallBack) : IRequest<OneOf<bool, Error>>;
}
