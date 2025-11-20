using FluentValidation;

namespace Application.Features.Users.Commands.UnlockUser
{
    public class UnlockUserCommandValidator : AbstractValidator<UnlockUserCommand>
    {
        public UnlockUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}