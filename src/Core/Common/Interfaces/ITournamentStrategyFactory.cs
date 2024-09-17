
namespace Core.Common.Interfaces
{
    public interface ITournamentStrategyFactory
    {
        public ITournamentStrategy GetStrategy(string gender);
    }
}
