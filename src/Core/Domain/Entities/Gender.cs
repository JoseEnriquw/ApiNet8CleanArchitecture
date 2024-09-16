
namespace Core.Domain.Entities
{
    public class Gender
    {
        public Gender()
        {
            Tournaments = [];
            Players = [];
        }
        public int Id { get; set; }
        public string Description { get; set; } = null!;

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
