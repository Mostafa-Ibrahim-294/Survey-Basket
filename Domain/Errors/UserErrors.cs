using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class UserErrors
    {
        public static Error InvalidCredentials => new("User.InvalidCredentials", "The provided credentials are invalid." , HttpStatusCode.Unauthorized);
        public static Error InvalidRefreshToken => new("User.InvalidRefreshToken", "The provided refresh token is invalid." , HttpStatusCode.Unauthorized);
        public static Error UserNotFound => new("User.NotFound", "The specified user was not found." , HttpStatusCode.NotFound);
        public static Error EmailAlreadyExists => new("User.EmailAlreadyExists", "The provided email is already associated with an existing account." , HttpStatusCode.Conflict);
        public static Error EmailAlreadyConfirmed => new("User.EmailAlreadyConfirmed", "The email address has already been confirmed." , HttpStatusCode.Conflict);
        public static Error FailedChangePassword => new("User.FailedChangePassword", "Failed to change the password." , HttpStatusCode.BadRequest);
        public static Error UserDisabled => new("User.Disabled", "The user account is disabled." , HttpStatusCode.Forbidden);
        public static Error NotConfirmedEmail => new("User.NotConfirmedEmail", "The user email is not confirmed." , HttpStatusCode.Forbidden);
        public static Error LockedUser => new("User.Locked", "The user account is locked due to multiple failed login attempts." , HttpStatusCode.Forbidden);
    }
}
