using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Authentication;
using Application.Contracts.Repositories;
using Application.Features.Questions.Dtos;
using AutoMapper;
using Domain.Errors;
using MediatR;
using OneOf;

namespace Application.Features.Questions.Queries.GetAvailableQuestions
{
    internal class GetAvailableQuestionsQueryHandler : IRequestHandler<GetAvailableQuestionsQuery, OneOf<IEnumerable<QuestionDto>, Error>>
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly IPollRepository _pollRepository;
        private readonly IUserContext _userContext;
        public GetAvailableQuestionsQueryHandler(
            IQuestionRepository questionRepository,
            IVoteRepository voteRepository,
            IPollRepository pollRepository,
            IUserContext userContext)
        {
            _questionRepository = questionRepository;
            _voteRepository = voteRepository;
            _pollRepository = pollRepository;
            _userContext = userContext;
        }
        public async Task<OneOf<IEnumerable<QuestionDto>, Error>> Handle(GetAvailableQuestionsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetCurrentUser()?.UserId;
            if (userId == null)
            {
                return UserErrors.UserNotFound;
            }
            var hasVoted = await _voteRepository.HasUserVotedAsync(userId, request.PollId, cancellationToken);
            if (hasVoted)
            {
                return VoteErrors.UserAlreadyVoted;
            }
            var isCurrentPoll = await _pollRepository.IsCurrentPoll(request.PollId, cancellationToken);
            if (!isCurrentPoll)
            {
                return PollErrors.NotFound;
            }
            var questions = await _questionRepository.GetAvailableAsync(request.PollId,cancellationToken);
            return questions.ToList();
        }
    }
}
