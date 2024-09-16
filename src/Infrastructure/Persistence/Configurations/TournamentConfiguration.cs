using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).ValueGeneratedOnAdd();
            builder.Property(t => t.StartDate).IsRequired();
            builder.Property(t => t.EndDate);
            builder.HasOne(t => t.Gender).WithMany(x=> x.Tournaments).HasForeignKey(t => t.GenderId);
            builder.HasOne(t => t.Winner).WithMany(x=> x.Tournaments).HasForeignKey(t => t.WinnerPlayerId);
        }
    }
}
