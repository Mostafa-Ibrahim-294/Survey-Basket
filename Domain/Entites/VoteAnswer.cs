using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class VoteAnswer
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public Vote Vote { get; set; } = null!;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public int AnswerId { get; set; }
        public Answer Answer { get; set; } = null!;
    }
}
