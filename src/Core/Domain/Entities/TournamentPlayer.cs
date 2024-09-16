namespace Core.Domain.Entities
{
    public class TournamentPlayer
    {
        public int TournamentId { get; set; }
        public int PlayerId { get; set; }

        public virtual Tournament Tournament { get; set; } = null!;
        public virtual Player Player { get; set; } = null!;
    }
}
