using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class MatchConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("match");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();
            builder.Property(m => m.DateMatch).IsRequired();
            builder.HasOne(m => m.Player1).WithMany().HasForeignKey(m => m.Player1Id);
            builder.HasOne(m => m.Player2).WithMany().HasForeignKey(m => m.Player2Id);
            builder.HasOne(m => m.Winner).WithMany().HasForeignKey(m => m.WinnerId);
            builder.HasOne(m => m.Tournament).WithMany(t => t.Matches).HasForeignKey(m => m.TournamentId);
        }
    }
}
