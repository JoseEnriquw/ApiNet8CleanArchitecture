using Core.Common.Interfaces;
using Core.Common.Models.Factories;
using Core.Common.Models.Strategies;
using Infrastructure.Options;
using Infrastructure.Persistence;
using Infrastructure.Repository;
using Infrastructure.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Boopstrap
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetSection("DBConnection").Get<Connection>();
            services.AddDbContextPool<ApplicationDbContext>(options =>
            {
                options.UseMySql(connection?.ConnectionString, new MySqlServerVersion(connection?.Version));
            });
            services.AddScoped<ApplicationDbContext>(); ;
            services.AddScoped<IRepositoryEF, RepositoryEF>();

            services.AddSingleton<ITournamentStrategyFactory, TournamentStrategyFactory>();
            services.AddSingleton(typeof(MaleTournamentStrategy));
            services.AddSingleton(typeof(FemaleTournamentStrategy));
            services.AddScoped<ITournamentService, TournamentService>();
            services.AddSingleton<IPaginationService, PaginationService>();


            return services;
        }
    }
}
