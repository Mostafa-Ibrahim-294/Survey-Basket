using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
