using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Subscriptions.Commands.CreateSubscription;
using Application.Features.Users.Dtos;
using Domain.Entites;

namespace Application.Contracts.Payment
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentSessionAsync(Subscription subscription);
    }
}
