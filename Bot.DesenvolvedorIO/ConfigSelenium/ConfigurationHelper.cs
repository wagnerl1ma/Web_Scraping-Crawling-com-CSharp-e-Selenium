using Microsoft.Extensions.Configuration;

namespace Bot.DesenvolvedorIO
{
    public class ConfigurationHelper
    {
        private readonly IConfiguration _config;

        public ConfigurationHelper()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public string WebDrivers => $"{_config.GetSection("WebDrivers").Value}";
        public string DomainUrl => _config.GetSection("DomainUrl").Value;
        public string LoginUrl => $"{DomainUrl}{_config.GetSection("LoginUrl").Value}";
        public string RegisterUrl => $"{DomainUrl}{_config.GetSection("RegisterUrl").Value}";
        public string DashBoard => $"{DomainUrl}{_config.GetSection("DashBoard").Value}";
        public string CursosUrl => _config.GetSection("CursosUrl").Value;
        

        public string? FolderPath => Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
        public string FolderPicture => $"{FolderPath}{_config.GetSection("FolderPicture").Value}";
    }
}
