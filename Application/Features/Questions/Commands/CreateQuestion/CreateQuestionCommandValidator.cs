using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
       public CreateQuestionCommandValidator()
       {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required.")
                .Length(3, 100);

           RuleFor(x => x.Answers)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("At least one answer is required.")
               .Must(x => x.Count() >= 1)
               .WithMessage("At least one answer is required.");
           RuleFor(x => x.Answers)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
               .Must(answers => answers.Distinct(StringComparer.OrdinalIgnoreCase).Count() == answers.Count())
               .WithMessage("Answers must be unique.");
        }
    }
}
