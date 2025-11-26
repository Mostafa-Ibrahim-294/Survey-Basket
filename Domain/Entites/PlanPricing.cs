using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entites
{
    public sealed class PlanPricing
    {
        public int Id { get; set; }

        public decimal Price { get; set; }  
        public string Currency { get; set; } = string.Empty;
        public BillingCycle BillingCycle { get; set; }
        public decimal Discount { get; set; }     

        public Guid PlanId { get; set; }
        public Plan Plan { get; set; } = null!;
    }
}
