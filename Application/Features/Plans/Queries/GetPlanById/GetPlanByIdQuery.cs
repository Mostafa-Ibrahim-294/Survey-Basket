using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Plans.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Plans.Queries.GetPlanById
{
    public sealed record GetPlanByIdQuery(int Id) : IRequest<OneOf<PlanDto, Error>>;
}
