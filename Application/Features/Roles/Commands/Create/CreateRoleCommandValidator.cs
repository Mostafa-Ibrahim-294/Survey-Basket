using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Roles.Commands.Create
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.");
        }
    }
}
