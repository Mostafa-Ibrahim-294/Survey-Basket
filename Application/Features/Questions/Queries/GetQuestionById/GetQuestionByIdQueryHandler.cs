using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Questions.Dtos;
using Domain.Errors;
using MediatR;
using OneOf;
using Application.Contracts;
using AutoMapper;

namespace Application.Features.Questions.Queries.GetQuestionById
{
    internal class GetQuestionByIdQueryHandler : IRequestHandler<GetQuestionByIdQuery, OneOf<QuestionDto, Error>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;   
        public GetQuestionByIdQueryHandler(IQuestionRepository questionRepository, IPollRepository pollRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _pollRepository = pollRepository;
            _mapper = mapper;
        }
        public async Task<OneOf<QuestionDto, Error>> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.PollId, cancellationToken);
            if (poll is null)
            {
                return PollErrors.NotFound;
            }
            var question = await _questionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (question is null)
            {
                return QuestionErrors.NotFound;
            }
            return _mapper.Map<QuestionDto>(question);
        }
    }
}
