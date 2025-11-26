using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts.Repositories;
using Domain.Entites;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _context;
        public PlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Plans.Include(p => p.PlanFeatures)
                                       .Include(p => p.PlanPricings)
                                       .ToListAsync(cancellationToken);
        }

        public async Task<Plan?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Plans.Include(p => p.PlanFeatures)
                                       .Include(p => p.PlanPricings)
                                       .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
