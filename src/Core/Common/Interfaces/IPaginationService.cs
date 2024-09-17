using Core.Common.Models;

namespace Core.Common.Interfaces
{
    public interface IPaginationService
    {
        public Task<PaginatedList<T>> CreateAsync<T>(IQueryable<T> source, int pageIndex, int pageSize);

        public PaginatedList<T> Create<T>(IEnumerable<T> source, int pageIndex, int pageSize);
    }
}
