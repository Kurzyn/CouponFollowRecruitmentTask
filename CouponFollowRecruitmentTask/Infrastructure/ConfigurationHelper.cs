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
            BrowserConfiguration = Configuration.GetSection(nameof(BrowserConfiguration)).Get<BrowserConfiguration>();
            AppDetails = Configuration.GetRequiredSection(nameof(AppDetails)).Get<AppDetails>();
        }

        public static IConfiguration Configuration => GetConfigurationForTests();

        public static BrowserConfiguration BrowserConfiguration { get; set; }

        public static AppDetails AppDetails { get; set; }
    }
}
