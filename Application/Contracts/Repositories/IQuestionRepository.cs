using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Questions.Dtos;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionDto>> GetAllByPollAsync(int pollId, CancellationToken cancellationToken = default);
        Task<Question?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Question> CreateAsync(Question question, CancellationToken cancellationToken = default);
        Task UpdateAsync(Question question, CancellationToken cancellationToken = default);
        Task<bool> IsContentExistInPollAsync(int pollId, string content, CancellationToken cancellationToken = default);
        Task<bool> IsContentExistForOtherIdInPollAsync(int pollId, string content, int id, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}