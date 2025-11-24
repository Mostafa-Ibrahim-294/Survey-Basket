using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    internal class VoteConfigurations : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasIndex(x => new {x.PollId , x.UserId}).IsUnique();
            builder.HasOne(x => x.Poll)
                .WithMany(x => x.Votes)
                .HasForeignKey(x => x.PollId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
