
namespace Core.Domain.Entities
{
    public class Tournament
    {
        public Tournament()
        {
            Matches = [];
            TournamentPlayers = [];
        }

        public int Id { get; set; }
        public int GenderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int WinnerPlayerId { get; set; }

        public virtual Player Winner { get; set; } = null!;
        public virtual Gender Gender { get; set; } = null!;
        public virtual ICollection<TournamentPlayer> TournamentPlayers { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
    }
}
