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
    internal class AnswerConfigurations : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasIndex(x => new { x.QuestionId, x.Content }).IsUnique();
            builder.Property(x => x.Content).HasMaxLength(1000).IsRequired();
            builder.HasOne(x => x.Question).WithMany(x => x.Answers).HasForeignKey(x => x.QuestionId);
        }
    }
}
