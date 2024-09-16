using Core.Common.Interfaces;
using Core.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Common.Factories
{
    public class TournamentStrategyFactory(IServiceProvider serviceProvider) : ITournamentStrategyFactory
    {
        public ITournamentStrategy GetStrategy(EGender gender)
        {
            var genderString=gender.ToString();
            var strategyType = Type.GetType($"GithubWizardApi.Application.Common.Models.RepositoryCreationStrategy.{genderString.ToString()}RepositoryCreationStrategy");

            if (strategyType == null || !typeof(ITournamentStrategy).IsAssignableFrom(strategyType))
            {
                throw new NotSupportedException($"Gender {genderString} is not supported.");
            }

            return (ITournamentStrategy)serviceProvider.GetRequiredService(strategyType);
        }
    }
}
