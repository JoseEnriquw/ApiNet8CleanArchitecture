namespace Core.Domain.Entities
{
    public class Player
    {
        public Player()
        {
            TournamentPlayers = [];
            Tournaments = [];
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Skill { get; set; }
        public int Strength { get; set; } = 0;
        public int Speed { get; set; }= 0;
        public int Reaction { get; set; } = 0;
        public int GenderId { get; set; } = 0;

        public virtual Gender Gender { get; set; }= null!;
        public virtual ICollection<TournamentPlayer> TournamentPlayers { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
