using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public sealed class Vote
    {
        public int Id { get; set; }
        public int PollId { get; set; }
        public Poll Poll { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;
        public ICollection<VoteAnswer> VoteAnswers { get; set; } = new List<VoteAnswer>();
    }
}
