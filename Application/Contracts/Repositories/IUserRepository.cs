using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;

namespace Application.Contracts.Repositories
{
    // Projection used by the repository (kept in Contracts to avoid referencing Application.Features DTOs from Infrastructure)
   
    public interface IUserRepository
    {
        Task<IReadOnlyList<UserResponse>> GetAllWithRolesAsync(CancellationToken cancellationToken = default);
    }
}