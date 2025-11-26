using System;
using Application.Features.Subscriptions.Commands.CreateSubscription;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Subscriptions.Dtos
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<CreateSubscriptionCommand, Subscription>();
        }
    }
}