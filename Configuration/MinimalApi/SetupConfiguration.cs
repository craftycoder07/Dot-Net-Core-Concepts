namespace MinimalApi
{
    public static class SetupConfiguration
    {
        public static void SetupAppConfiguration(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((context, config) => {
                IHostEnvironment env = context.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true);
            });
        }
    }
}