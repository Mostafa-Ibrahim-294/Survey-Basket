using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Votes.Dtos
{
    public class PollVoteDto
    {
        public string Title { get; set; } = null!;  
        public IEnumerable<VoteDto> Votes { get; set; } = null!;
    }
}
