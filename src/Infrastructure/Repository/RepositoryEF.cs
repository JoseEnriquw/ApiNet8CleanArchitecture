using Core.Common.Interfaces;
using Core.Common.Models;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.UseCase.V1.TournamentOperations.Queries.GetAll;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public class RepositoryEF : IRepositoryEF
    {
        private readonly ILogger<RepositoryEF> _logger;
        private readonly ApplicationDbContext _dbContext;
        private readonly IPaginationService _pagination;
        public RepositoryEF(ILogger<RepositoryEF> logger, ApplicationDbContext dbContext,IPaginationService pagination)
        {
            _logger = logger;
            _dbContext = dbContext;
            _pagination = pagination;

        }

        public void Insert<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Add(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting entity {entity}", entity);
                throw;
            }
        }

        public void Update<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Attach(entity).State = EntityState.Modified;
                _dbContext.Update(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity {entity}", entity);
                throw;
            }
        }

        public void Delete<T>(T entity) where T : class
        {
            try
            {
                _dbContext.Remove(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity {entity}", entity);
                throw;
            }
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all ");
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving changes");
                throw;
            }
        }

        public async Task<T> FindAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            try
            {
                return await _dbContext.Set<T>().FirstAsync(func);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error finding entity ");
                throw;
            }
        }      
        
        public async Task<List<Player>> GetPlayersByIdsAsync(List<int> ids, int genderId) 
        {
           return await _dbContext.Players.Include(x=> x.Gender).Where(x=> ids.Contains(x.Id) && x.GenderId==genderId).ToListAsync();
        }

        public async Task<PaginatedList<TournamentDto>> GetTorneosByFiltersAsync(GetTournametsByFilters filter)
        {
            var tournaments = _dbContext.Tournaments
                .Include(x => x.Winner)
                .ThenInclude(x => x.Gender)
                .Include(x => x.TournamentPlayers)
                .ThenInclude(x => x.Player)
                .ThenInclude(x=> x.Gender)
                .Include(x => x.Matches)
                .Include(x=> x.Gender)
                .Include("Matches.Winner")
                .Include("Matches.Winner.Gender")
                .Include("Matches.Player1")
                .Include("Matches.Player1.Gender")
                .Include("Matches.Player2")  
                .Include("Matches.Player2.Gender")
                .AsQueryable();

            if(filter.StartDate != null)
            {
                tournaments = tournaments.Where(x => x.StartDate.Day == filter.StartDate.GetValueOrDefault().Day 
                && x.StartDate.Month == filter.StartDate.GetValueOrDefault().Month
                && x.StartDate.Year == filter.StartDate.GetValueOrDefault().Year);
            }

            if (filter.Gender != null)
            {
                tournaments= tournaments.Where(x => x.GenderId == filter.Gender);
            }

            var tournamentsDto= tournaments.Select(x => new TournamentDto
            {
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Gender = x.Gender.Description,
                Players = x.TournamentPlayers.Select(x => new PlayerDto
                {
                    Name = x.Player.Name,
                    Reaction = x.Player.Reaction,
                    Skill = x.Player.Skill,
                    Speed = x.Player.Speed,
                    Strength = x.Player.Strength,
                    Gender = x.Player.Gender.Description
                }).ToList(),
                Matches = x.Matches.Select(x => new MatchDto
                {
                    DateMatch = x.DateMatch,
                    Player1 = x.Player1,
                    Player2 = x.Player2,
                    Winner = x.Winner.Name,

                }).ToList(),
                Winner = x.Winner
            });

            return await _pagination.CreateAsync(tournamentsDto, filter.Page, filter.Size);
        }
    }
}
