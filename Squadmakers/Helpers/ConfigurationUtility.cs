namespace Squadmakers.Helpers
{
    public static class ConfigurationUtility
    {
        public static string? GetConnectionString(IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("SQUADMAKERS_CONECTION_BD");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                connectionString = configuration.GetConnectionString("DefaultConnection");
            }

            return connectionString;
        }
    }
}
