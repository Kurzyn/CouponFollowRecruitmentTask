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
        //public static BrowserConfiguration BrowserConfig => Configuration.GetSection(nameof(BrowserConfiguration));
        public static IConfiguration Configuration => GetConfigurationForTests();

    }
}
