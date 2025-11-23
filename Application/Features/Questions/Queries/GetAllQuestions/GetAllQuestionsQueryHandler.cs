using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Questions.Dtos;
using AutoMapper;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Queries.GetAllQuestions
{
    internal class GetAllQuestionsQueryHandler : IRequestHandler<GetAllQuestionsQuery, OneOf<IEnumerable<QuestionDto>, Error>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IPollRepository _pollRepository;
        public GetAllQuestionsQueryHandler(IQuestionRepository questionRepository, IPollRepository pollRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _pollRepository = pollRepository;
        }
        public async Task<OneOf<IEnumerable<QuestionDto>, Error>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.PollId, cancellationToken);
            if (poll is null)
            {
                return PollErrors.NotFound;
            }

            var questions = await _questionRepository.GetAllByPollAsync(poll.Id, cancellationToken);
            return questions.ToList();
        }
    }
}
