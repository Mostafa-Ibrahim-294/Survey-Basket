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
    internal class QuestionConfigurations : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasIndex(q => new { q.PollId, q.Content }).IsUnique();
            builder.Property(q => q.Content).IsRequired().HasMaxLength(1000);
            builder.HasOne(q => q.Poll)
                .WithMany(p => p.Questions)
                .HasForeignKey(q => q.PollId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(q => q.CreatedBy)
                .WithOne()
                .HasForeignKey<Question>(q => q.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
