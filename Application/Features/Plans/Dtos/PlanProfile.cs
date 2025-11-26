using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Plans.Dtos
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<Plan, PlanDto>();
            CreateMap<PlanPricing, PlanPricingDto>();
            CreateMap<PlanFeature, PlanFeatureDto>();
        }
    }
}
