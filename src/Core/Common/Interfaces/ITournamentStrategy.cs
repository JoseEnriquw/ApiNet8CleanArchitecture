using Core.Domain.Entities;

namespace Core.Common.Interfaces
{
    public interface ITournamentStrategy
    {
        Player DetermineWinner(Player player1, Player player2);
    }
}
