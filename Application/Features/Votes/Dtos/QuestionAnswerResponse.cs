using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Dtos
{
    public class QuestionAnswerResponse
    {
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
    }
}
