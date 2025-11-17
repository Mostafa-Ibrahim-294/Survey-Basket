using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class PollErrors
    {
        public static Error NotFound => new("Poll.NotFound", "The specified poll was not found.");
        public static Error DuplicateTitle => new("Poll.DuplicateTitle", "A poll with the same title already exists.");
    }
}
