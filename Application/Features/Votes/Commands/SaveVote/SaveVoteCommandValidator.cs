using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Votes.Commands.SaveVote
{
    public class SaveVoteCommandValidator : AbstractValidator<SaveVoteCommand>
    {
        public SaveVoteCommandValidator()
        {
            RuleFor(x => x.Answers)
                .NotEmpty()
                .WithMessage("At least one answer must be provided.");

            RuleForEach(x => x.Answers).ChildRules(answer =>
            {
                answer.RuleFor(x => x.QuestionId)
                    .GreaterThan(0)
                    .WithMessage("Invalid question ID.");

                answer.RuleFor(x => x.AnswerId)
                    .GreaterThan(0)
                    .WithMessage("Invalid answer ID.");
            });
        }
    }
}
