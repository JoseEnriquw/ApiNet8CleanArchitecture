﻿using Core.Common.Interfaces;
using Core.Domain.Entities;

namespace Core.Common.Strategies
{
    public class FemaleTournamentStrategy : ITournamentStrategy
    {
        private readonly Random _random = new();

        public Player DetermineWinner(Player player1, Player player2)
        {
            int score1 = player1.Skill + player1.Reaction + _random.Next(0, 10);
            int score2 = player2.Skill + player2.Reaction + _random.Next(0, 10);
            return score1 > score2 ? player1 : player2;
        }
    }
}
