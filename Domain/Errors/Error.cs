using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    // add status code field

    public record Error(string Code , string Message , HttpStatusCode StatusCode)
    {
        public static Error None => new Error(string.Empty, string.Empty , HttpStatusCode.OK);
    }
}
