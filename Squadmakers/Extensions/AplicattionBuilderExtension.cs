using Microsoft.EntityFrameworkCore;
using Squadmakers.Infraestructure.Models;

namespace Squadmakers.Extensions
{
    public static class AplicattionBuilderExtension
    {
        public static IApplicationBuilder ApplyDatabaseMigrationsSiguda(this IApplicationBuilder app, IConfiguration configuration)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<SquadmakersContext>();
            if (configuration.GetValue<bool>("DatabaseOptions:MigrateAndCreateOnStartup"))
            {
                context?.Database.EnsureCreated();
                context?.Database.Migrate();
            }
            return app;
        }
    }
}
