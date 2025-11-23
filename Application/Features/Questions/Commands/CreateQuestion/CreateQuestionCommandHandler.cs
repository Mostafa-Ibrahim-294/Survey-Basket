using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Questions.Dtos;
using AutoMapper;
using Domain.Entites;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Commands.CreateQuestion
{
    internal class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, OneOf<QuestionDto, Error>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IPollRepository _pollRepository;
        private readonly IMapper _mapper;
        public CreateQuestionCommandHandler(IQuestionRepository questionRepository, IMapper mapper , IPollRepository pollRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _pollRepository = pollRepository;
        }
        public async Task<OneOf<QuestionDto, Error>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var poll = await _pollRepository.GetByIdAsync(request.PollId);
            if (poll is null)
            {
                return PollErrors.NotFound;
            }
            var isDuplicate = await _questionRepository.IsContentExistInPollAsync(request.PollId, request.Content, cancellationToken);
            if (isDuplicate)
            {
                return QuestionErrors.DuplicateContent;
            }
            var question = _mapper.Map<Question>(request);
            await _questionRepository.CreateAsync(question, cancellationToken);
            var questionDto = _mapper.Map<QuestionDto>(question);
            return questionDto;
        }
    }
}
