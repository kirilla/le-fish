namespace Lefish.Web
{
    public class ConnectionStringFactory
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ConfigurationManager _configurationManager;

        public ConnectionStringFactory(
            IWebHostEnvironment environment,
            ConfigurationManager configurationManager) 
        {
            _environment = environment;
            _configurationManager = configurationManager;

#if LOCAL || DEBUG
            _environment.EnvironmentName = "Local";
#elif PRODUCTION || RELEASE
            _environment.EnvironmentName = "Production";
#elif REMOTE
            _environment.EnvironmentName = "Remote";
#else
            throw new Exception(
                $"Unable to decide on a database connection string " +
                $"for unknown build configuration.");
#endif
        }

        public string? GetConnectionString()
        {
            return _configurationManager[
                $"ConnectionStrings:{_environment.EnvironmentName}"];
        }

        /*
         * Purpose: 
         * 
         * Get the database connection string from the Config/Database.json file
         * which is purposefully not commited to the source code repository, 
         * unlike the appsettings.json files, which are.
         * 
         */
    }
}
