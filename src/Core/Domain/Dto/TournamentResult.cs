
namespace Core.Domain.Dto
{
    public class TournamentResult
    {
        public PlayerDto Winner { get; set; } = null!;
        public int MatchCount { get; set; }
    }
}
