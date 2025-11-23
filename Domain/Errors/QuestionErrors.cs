using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class QuestionErrors
    {
        public static Error DuplicateContent => new Error(
            Code: "Question.DuplicateContent",
            Message: "A question with the same content already exists in this poll.",
            StatusCode: HttpStatusCode.Conflict
        );
        public static Error NotFound => new Error(
            Code: "Question.NotFound",
            Message: "The specified question was not found.",
            StatusCode: HttpStatusCode.NotFound
        );
    }
}
