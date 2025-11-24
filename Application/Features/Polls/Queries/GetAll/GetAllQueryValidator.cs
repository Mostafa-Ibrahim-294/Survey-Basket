using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Application.Features.Polls.Queries.GetAll
{
    public class GetAllQueryValidator : AbstractValidator<GetAllQuery>
    {
        public GetAllQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.")
                .LessThanOrEqualTo(1000).WithMessage("Page number must not exceed 1000.")
                .When(x => x is not null);

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(500).WithMessage("Page size must not exceed 500.")
                .When(x => x is not null);

        }
    }
}
