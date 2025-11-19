using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class RoleErrors
    {
        public static Error RoleAlreadyExists => new("Role.AlreadyExists", "A role with the same name already exists.", HttpStatusCode.Conflict);
        public static Error RoleCreationFailed => new("Role.CreationFailed", "Failed to create the role due to an internal error.", HttpStatusCode.InternalServerError);
        public static Error RoleNotFound => new("Role.NotFound", "Failed to Find the role .", HttpStatusCode.NotFound);

    }
}
