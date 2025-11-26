using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Plans.Dtos
{
    public class PlanFeatureDto
    {
        public int Id { get; set; }
        public int LimitValue { get; set; }
        public string LimitUnit { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
    }
}
