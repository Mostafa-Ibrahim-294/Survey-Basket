using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Roles.Commands.Update
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Role name is required.")
                .MaximumLength(100).WithMessage("Role name must not exceed 100 characters.");
        }
    }
}
