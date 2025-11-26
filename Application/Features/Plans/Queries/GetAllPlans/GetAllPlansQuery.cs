using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Plans.Dtos;
using MediatR;

namespace Application.Features.Plans.Queries.GetAllPlans
{
    public sealed record GetAllPlansQuery : IRequest<IEnumerable<PlanDto>>;
}
