using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class PlanErrors
    {
        public static Error PlanNotFound => new Error("Plan.NotFound", "The specified plan was not found.", HttpStatusCode.NotFound);
    }
}
