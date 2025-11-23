using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Questions.Dtos
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
