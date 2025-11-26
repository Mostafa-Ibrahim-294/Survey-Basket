using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites;

    public sealed class Plan
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PlanPricing> PlanPricings { get; set; } = new List<PlanPricing>();
        public ICollection<PlanFeature> PlanFeatures { get; set; } = new List<PlanFeature>();
}
