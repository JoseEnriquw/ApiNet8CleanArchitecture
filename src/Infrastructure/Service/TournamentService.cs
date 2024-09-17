using Core.Common.Interfaces;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;

namespace Infrastructure.Service
{
    public class TournamentService(ITournamentStrategyFactory factory,IRepositoryEF repository) : ITournamentService
    {
        private ITournamentStrategy? _strategy;
        public async Task<TournamentResult> PlayTournament(List<Player> players,EGender gender)
        {
            _strategy= factory.GetStrategy(gender.ToString());
            if (_strategy == null) throw new NullReferenceException("ITournamentStrategy cannot be null");

            var tournament = new Tournament()
            {
                StartDate = DateTime.Now,
                GenderId = (int)gender!,
                TournamentPlayers= players.Select(x=> new TournamentPlayer
                {
                    PlayerId=x.Id,
                }).ToList(),
                Matches = []
            };
            var winner = SimulateRounds(players, tournament);

            tournament.EndDate = DateTime.Now;
            tournament.WinnerPlayerId = winner.Id;

            repository.Insert(tournament);
            await repository.SaveChangesAsync();

            return new()
            {
                Winner = winner,
                MatchCount= players.Count - 1,
            };
        }

        private Player SimulateRounds(List<Player> players, Tournament tournament)
        {
            if (players.Count == 1)
            {
                return players[0];
            }

            List<Player> nextRoundPlayers = [];
            for (int i = 0; i < players.Count; i += 2)
            {
                var winner = _strategy!.DetermineWinner(players[i], players[i + 1]);

                tournament.Matches.Add( new Match
                {
                    Player1Id = players[i].Id,
                    Player2Id = players[i + 1].Id,
                    WinnerId = winner.Id,
                    DateMatch = DateTime.Now
                });

                nextRoundPlayers.Add(winner);
            }

            return SimulateRounds(nextRoundPlayers, tournament);
        }
    }
}
