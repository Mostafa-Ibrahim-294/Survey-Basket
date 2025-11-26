using Application.Features.Subscriptions.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Subscriptions.Queries.GetCurrentSubscription
{
    public record GetCurrentSubscriptionQuery : IRequest<OneOf<SubscriptionDto, Error>>;
}