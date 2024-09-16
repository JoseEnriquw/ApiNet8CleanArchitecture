using Core.Common.Interfaces;
using Core.Domain.Classes;
using Core.Domain.Common;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using MediatR;
using System.Net;

namespace Core.UseCase.V1.TournamentOperations.Command.Create
{
    public class CreateAndPlayTournamentCommand : IRequest<Response<TournamentResult>>
    {
        public int Gender { get; set; }
        public List<int> PlayersId { get; set; } = null!;
    }

    public class CreateAndPlayTournamentCommandHandler(IRepositoryEF repository,ITournamentService tournamentService) : IRequestHandler<CreateAndPlayTournamentCommand, Response<TournamentResult>>
    {
        public async Task<Response<TournamentResult>> Handle(CreateAndPlayTournamentCommand request, CancellationToken cancellationToken)
        {
            var players = await repository.WhereAsync<Player>(x => request.PlayersId.Any(p => x.Id == p));
            var response= new Response<TournamentResult>();
            if (players.Count != request.PlayersId.Count) 
            {
                request.PlayersId.Where(x => players.All(p => x != p.Id)).ToList().ForEach(x =>
                {
                    response.AddNotification("#123", nameof(request.PlayersId), string.Format(ErrorMessage.NOT_FOUND_GET_BY_ID, x, nameof(Player)));
                    response.StatusCode = HttpStatusCode.BadRequest;
                });
            }
            else
            {
                response.Content= await tournamentService.PlayTournament(players,(EGender)request.Gender);
                response.StatusCode = HttpStatusCode.OK;    
            }

            return response;
        }
    }
}
