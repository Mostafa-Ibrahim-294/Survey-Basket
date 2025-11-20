using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Application.Features.Users.Dtos;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<UserResponse>> GetAllWithRolesAsync(CancellationToken cancellationToken = default)
        {
            var flat = await (
                from u in _context.Users.AsNoTracking()
                join ur in _context.UserRoles.AsNoTracking() on u.Id equals ur.UserId into urj
                from ur in urj.DefaultIfEmpty()
                join r in _context.Roles.AsNoTracking() on ur.RoleId equals r.Id into rj
                from r in rj.DefaultIfEmpty()
                select new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.IsDisabled,
                    RoleName = r != null ? r.Name : null
                }
            ).ToListAsync(cancellationToken);

            var result = flat
                .GroupBy(x => new { x.Id, x.FirstName, x.LastName, x.Email, x.IsDisabled })
                .Select(g => new UserResponse
                {
                    Id = g.Key.Id,
                    FirstName = g.Key.FirstName ?? string.Empty,
                    LastName = g.Key.LastName ?? string.Empty,
                    Email = g.Key.Email ?? string.Empty,
                    IsDisabled = g.Key.IsDisabled,
                    Roles = g.Where(x => x.RoleName != null)
                             .Select(x => x.RoleName!)
                             .Distinct()
                             .ToList()
                })
                .ToList()
                .AsReadOnly();

            return result;
        }
    }
}