using Core.Common.Factories;
using Core.Common.Interfaces;
using Core.Common.Strategies;
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

            services.AddScoped<ITournamentStrategyFactory, TournamentStrategyFactory>();
            services.AddScoped<ITournamentStrategy, MaleTournamentStrategy>();
            services.AddScoped<ITournamentStrategy, FemaleTournamentStrategy>();
            services.AddScoped<ITournamentService, TournamentService>();


            return services;
        }
    }
}
