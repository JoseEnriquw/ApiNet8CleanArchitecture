using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Skill).IsRequired();
            builder.Property(p => p.Strength).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Speed).HasDefaultValue(0).IsRequired();
            builder.Property(p => p.Reaction).HasDefaultValue(0).IsRequired();
            builder.HasOne(t => t.Gender).WithMany(x => x.Players).HasForeignKey(t => t.GenderId);
        }
    }
}
