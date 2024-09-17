using Core.Common.Models;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.UseCase.V1.TournamentOperations.Queries.GetAll;
using System.Linq.Expressions;

namespace Core.Common.Interfaces
{
    public interface IRepositoryEF
    {
        void Insert<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;

        Task SaveChangesAsync();

        Task<List<T>> GetAllAsync<T>() where T : class;

        Task<T> FindAsync<T>(Expression<Func<T, bool>> func) where T : class;
        Task<List<Player>> GetPlayersByIdsAsync(List<int> ids,int genderId);
        Task<PaginatedList<TournamentDto>> GetTorneosByFiltersAsync(GetTournametsByFilters filter);
    }
}
