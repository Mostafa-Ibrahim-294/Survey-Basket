using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Dtos
{
    public record CurrentUser(string UserId,string Email, string FirstName, string LastName, IEnumerable<string> Roles);
}
