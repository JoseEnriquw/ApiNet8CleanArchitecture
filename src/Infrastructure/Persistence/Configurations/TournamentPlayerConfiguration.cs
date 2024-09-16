using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class TournamentPlayerConfiguration : IEntityTypeConfiguration<TournamentPlayer>
    {
        public void Configure(EntityTypeBuilder<TournamentPlayer> builder)
        {
            builder.HasKey(tp => new { tp.TournamentId, tp.PlayerId });
            builder.HasOne(tp => tp.Tournament).WithMany(t => t.TournamentPlayers).HasForeignKey(tp => tp.TournamentId);
            builder.HasOne(tp => tp.Player).WithMany(p => p.TournamentPlayers).HasForeignKey(tp => tp.PlayerId);
        }
    }
}
