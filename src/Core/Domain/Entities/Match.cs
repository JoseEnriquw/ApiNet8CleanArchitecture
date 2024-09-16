
namespace Core.Domain.Entities
{
    public class Match
    {      
        public int Id { get; set; }

        public int Player1Id { get; set; }

        public int Player2Id { get; set; }

        public int TournamentId { get; set; }
        public int WinnerId { get; set; }
        public DateTime DateMatch { get; set; }

        public virtual Player Player1 { get; set; }=null!;
        public virtual Player Player2 { get; set; } = null!;
        public virtual Player Winner { get; set; }=null!;
        public virtual Tournament Tournament { get; set; }=null!;
    }
}
