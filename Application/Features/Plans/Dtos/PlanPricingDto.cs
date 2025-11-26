using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Plans.Dtos
{
    public class PlanPricingDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string BillingCycle { get; set; } = string.Empty; 
        public decimal Discount { get; set; }
    }
}
