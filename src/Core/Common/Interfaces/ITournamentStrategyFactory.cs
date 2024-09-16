using Core.Domain.Enums;

namespace Core.Common.Interfaces
{
    public interface ITournamentStrategyFactory
    {
        public ITournamentStrategy GetStrategy(EGender gender);
    }
}
