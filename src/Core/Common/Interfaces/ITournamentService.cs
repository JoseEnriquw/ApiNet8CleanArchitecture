using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Core.Common.Interfaces
{
    public interface ITournamentService
    {
        Task<TournamentResult> PlayTournament(List<Player> players, EGender gender);
    }
}
