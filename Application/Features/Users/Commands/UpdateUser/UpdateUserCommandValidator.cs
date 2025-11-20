using FluentValidation;
using System.Collections.Generic;

namespace Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Roles)
                .NotNull()
                .Must(r => r is not null && Any(r))
                .WithMessage("At least one role must be specified.");
        }

        private static bool Any(IEnumerable<string> roles)
        {
            foreach (var _ in roles) return true;
            return false;
        }
    }
} 