using Asp.Versioning;
using Core.Common.Models;
using Core.Domain.Classes;
using Core.Domain.Dto;
using Core.UseCase.V1.TournamentOperations.Commands.Create;
using Core.UseCase.V1.TournamentOperations.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using PruebaApi.Helpers;
using PruebaApi.Models;

namespace PruebaApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TournamentController : ApiControllerBase
    {
        /// <summary>
        /// Creación y juego de un nuevo torneo
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(TournamentResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TournamentVM body) => Result(await Sender.Send(new CreateAndPlayTournamentCommand()
        {
            Gender = body.Gender,
            PlayersId = body.PlayersId
        }));

        /// Listado de Torneos por filtros y paginados
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<TournamentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] DateTime? startDate, int? page, int? size, int? gender) => Result(await Sender.Send(new GetTournametsByFilters()
        {
            StartDate = startDate,
            Gender=gender,
            Page = page ?? 1,
            Size = size ?? 10
        }));

    }
}
