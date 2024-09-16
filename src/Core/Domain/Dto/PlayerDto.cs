using Core.Domain.Entities;

namespace Core.Domain.Dto
{
    public class PlayerDto
    {
        public string Name { get; set; } = null!;
        public int Skill { get; set; }
        public int Strength { get; set; } = 0;
        public int Speed { get; set; } = 0;
        public int Reaction { get; set; } = 0;

        public static implicit operator PlayerDto(Player player)
        {
            return new() 
            {
                Name = player.Name, 
                Skill = player.Skill,
                Strength = player.Strength,
                Speed = player.Speed,
                Reaction= player.Reaction
            };
        }
    }
}
