using Core.Common.Interfaces;
using Core.Common.Models;
using Core.Domain.Classes;
using Core.Domain.Dto;
using MediatR;

namespace Core.UseCase.V1.TournamentOperations.Queries.GetAll
{
    public class GetTournametsByFilters : IRequest<Response<PaginatedList<TournamentDto>>>
    {
        public int? Gender { get; set; }
        public DateTime? StartDate { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }

    public class GetTournametsByFiltersHandler(IRepositoryEF repository) : IRequestHandler<GetTournametsByFilters, Response<PaginatedList<TournamentDto>>>
    {
        public async Task<Response<PaginatedList<TournamentDto>>> Handle(GetTournametsByFilters request, CancellationToken cancellationToken)
        {
            var tournaments = await repository.GetTorneosByFiltersAsync(request);

            return new()
            {
                Content = tournaments,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
