using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class VoteErrors
    {
        public static Error UserAlreadyVoted => new Error("Vote.UserAlreadyVoted", "The user has already voted in this poll." , HttpStatusCode.Conflict);
    }
}
