using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Commands.ToggleQuestion
{
    internal class ToggleQuestionCommandHandler : IRequestHandler<ToggleQuestionCommand, OneOf<bool, Error>>
    {
        private readonly IPollRepository _PollRepository;
        private readonly IQuestionRepository _questionRepository;
        public ToggleQuestionCommandHandler(IPollRepository pollRepository, IQuestionRepository questionRepository)
        {
            _PollRepository = pollRepository;
            _questionRepository = questionRepository;
        }
        public async Task<OneOf<bool, Error>> Handle(ToggleQuestionCommand request, CancellationToken cancellationToken)
        {
            var poll = await _PollRepository.GetByIdAsync(request.PollId, cancellationToken);
            if (poll is null)
            {
                return PollErrors.NotFound;
            }
            var question = await _questionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (question is null)
            {
                return QuestionErrors.NotFound;
            }
            question.IsActive = !question.IsActive;
            await _questionRepository.SaveAsync(cancellationToken);
            return true;
        }
    }
}
