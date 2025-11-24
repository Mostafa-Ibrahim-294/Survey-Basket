using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Dtos
{
    public class VoteDto
    {
        public string UserName { get; set; } = null!;
        public DateTime VoteDate { get; set; }
        public IEnumerable<QuestionAnswerResponse> QuestionAnswers { get; set; } = null!;
    }
}
