namespace KOG.ECommerce.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(environment))
                environment = "Production";

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environment}.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            return root;
        }

        public static string GetConnectionString(string name = "Default")
        {
            return GetConfigurationValue($"ConnectionStrings:{name}");
        }

        private static string GetConfigurationValue(string key)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            
            if (string.IsNullOrEmpty(environment))
                environment = "Production";

            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environment}.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();

            return root.GetSection(key).Value;
        }
    }
}
