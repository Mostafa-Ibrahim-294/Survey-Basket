using FluentValidation;

namespace Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
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