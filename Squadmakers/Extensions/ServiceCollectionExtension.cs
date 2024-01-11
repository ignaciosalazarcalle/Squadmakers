using Microsoft.EntityFrameworkCore;
using Squadmakers.Helpers;
using Squadmakers.Infraestructure.Models;

namespace Squadmakers.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddDbContextSquadmakers(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<DbContext, SquadmakersContext>()
                .AddDbContextPool<SquadmakersContext>(options => { LoadOptions(configuration, options); });
        }

        private static void LoadOptions(IConfiguration configuration, DbContextOptionsBuilder options)
        {
            var connectionString = ConfigurationUtility.GetConnectionString(configuration);
            options
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            options.UseSqlServer(connectionString);

            if (configuration.GetValue<bool>("DatabaseOptions:LogParamQueries"))
            {
                options.EnableSensitiveDataLogging();
            }
        }
    }
}
