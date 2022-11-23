using Microsoft.Extensions.Configuration;

namespace CouponFollowRecruitmentTask.Infrastructure
{
    public static class ConfigurationHelper
    {
        public static IConfiguration GetConfigurationForTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration;
        }

        static ConfigurationHelper()
        {
            browserConfiguration = new();
            Configuration.GetSection(nameof(BrowserConfiguration)).Bind(browserConfiguration);
        }

        public static IConfiguration Configuration => GetConfigurationForTests();

        public static BrowserConfiguration BrowserConfiguration { get => browserConfiguration; private set => browserConfiguration = value; }

        private static BrowserConfiguration browserConfiguration;
    }
}
