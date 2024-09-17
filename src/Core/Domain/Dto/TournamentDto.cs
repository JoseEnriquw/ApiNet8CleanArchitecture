

namespace Core.Domain.Dto
{
    public class TournamentDto   
    {
        public string Gender { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PlayerDto Winner { get; set; } = null!;
        public List<MatchDto> Matches { get; set; }=null!;
        public List<PlayerDto> Players { get; set; } = null!;
    }
}
