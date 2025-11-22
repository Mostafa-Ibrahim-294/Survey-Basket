using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Answer 
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
    }
}
