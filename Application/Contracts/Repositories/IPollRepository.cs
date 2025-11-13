using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IPollRepository
    {
        Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Poll?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Poll> CreateAsync(Poll poll, CancellationToken cancellationToken = default);
        Task UpdateAsync(Poll poll, CancellationToken cancellationToken = default);
        Task DeleteAsync(Poll poll, CancellationToken cancellationToken = default);
        Task SaveAsync(CancellationToken cancellationToken = default);
    }
}
