using Core.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Models.Factories
{
    public class TournamentStrategyFactory(IServiceProvider serviceProvider) : ITournamentStrategyFactory
    {
        public ITournamentStrategy GetStrategy(string gender)
        {
            var strategyType = Type.GetType($"Core.Common.Strategies.{gender}TournamentStrategy");

            if (strategyType == null || !typeof(ITournamentStrategy).IsAssignableFrom(strategyType))
            {
                throw new NotSupportedException($"Gender {gender} is not supported.");
            }

            return (ITournamentStrategy)serviceProvider.GetRequiredService(strategyType);
        }
    }
}
