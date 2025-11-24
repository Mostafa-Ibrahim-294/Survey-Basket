using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        internal DbSet<Poll> Polls { get; set; }
        internal DbSet<RefreshToken> RefreshTokens { get; set; }
        internal DbSet<Question> Questions { get; set; }
        internal DbSet<Answer> Answers { get; set; }
        internal DbSet<Vote> Votes { get; set; }
        internal DbSet<VoteAnswer> VoteAnswers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        override public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<Auditable>();
            var now = DateTime.UtcNow;
            foreach (var entry in entries)
            {
                var currentUserId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value!;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedById = currentUserId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedById = currentUserId;
                    entry.Entity.UpdatedOn = now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
