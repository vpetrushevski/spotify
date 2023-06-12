namespace Spotify.API.Config
{
    public class AppConfig
    {
        #region Public Static Properties

        public static IWebHostEnvironment HostingEnvironment { get; private set; }

        public static string ContentRootPath { get; private set; }

        public static IConfigurationRoot Configuration { get; private set; }
        #endregion

        #region Constructors

        public AppConfig(WebApplicationBuilder appBuilder)
        {
            #region Initialize Static Properties

            HostingEnvironment = appBuilder.Environment;
            ContentRootPath = HostingEnvironment.ContentRootPath;

            string currentConfig = string.Empty;

#if RELEASE
            currentConfig = "Production";
#else
            currentConfig = "Development";
#endif

            Configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", false, true)
                                .AddJsonFile($"appsettings.{currentConfig}.json", true)
                                .AddEnvironmentVariables()
                                .Build();
            #endregion
        }

        #endregion
    }
}
