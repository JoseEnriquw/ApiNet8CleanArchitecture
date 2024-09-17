
namespace Core.Domain.Dto
{
    public class MatchDto
    {
        public string Winner { get; set; } = null!;
        public PlayerDto Player1 { get; set; } = null!;
        public PlayerDto Player2 { get; set; } = null!;
        public DateTime DateMatch { get; set; }
    }
}
