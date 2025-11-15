using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;

namespace Application.Contracts.Repositories
{
    public interface IRefreshTokenRepository
    {
        public Task<RefreshToken?> GetRefreshTokenAsync(string refreshToken , CancellationToken cancellationToken);
    }
}
