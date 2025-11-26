using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Features.Plans.Dtos
{
    public class PlanDto
    {
        public int Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string slug { get; set; } = string.Empty;
        public IEnumerable<PlanPricingDto> PlanPricings { get; set; } = Enumerable.Empty<PlanPricingDto>();
        public IEnumerable<PlanFeatureDto> PlanFeatures { get; set; } = Enumerable.Empty<PlanFeatureDto>();
    }
}
