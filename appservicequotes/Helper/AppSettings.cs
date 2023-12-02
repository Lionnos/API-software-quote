using appservicequotes.DataTransferObject.AdditionalObject;

namespace appservicequotes.Helper
{
    public class AppSettings
    {
        public static DtoAppSettings dtoAppSettings;

        public static void Init()
        {
            dtoAppSettings = new DtoAppSettings();

            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            IConfigurationRoot configurationRoot = configurationBuilder.Build();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");

            configurationBuilder.AddJsonFile(path, false);
            configurationRoot = configurationBuilder.Build();

            dtoAppSettings.originRequest = configurationRoot.GetSection("GlobalSettings").GetSection("originRequest").Value;
            dtoAppSettings.jwtSecret = configurationRoot.GetSection("GlobalSettings").GetSection("jwtSecret").Value;
            //dtoAppSettings.connetionStringsSqlServer = configurationRoot.GetConnectionString("ConnectionStrings");
            //dtoAppSettings.connetionStringsSqlServer = configurationRoot.GetSection("ConnectionStrings").GetSection("connetionStringsSqlServer").Value;
        }
        
        public static string GetConnectionStringSqlServer()
        {
            return dtoAppSettings.connetionStringsSqlServer;
        }

        public static string GetOriginRequest()
        {
            return dtoAppSettings.originRequest;
        }

        public static string GetJwtSecret()
        {
            return dtoAppSettings.jwtSecret;
        }
    }
}
