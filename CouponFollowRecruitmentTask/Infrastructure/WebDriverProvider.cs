using Ninject.Activation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;

namespace CouponFollowRecruitmentTask.Infrastructure
{
    internal class WebDriverProvider : Provider<IWebDriver>
    {

        protected override IWebDriver CreateInstance(IContext context)
        {
            return ConfigurationHelper.BrowserConfiguration.BrowserType switch
            {
                BrowserType.Chrome => new ChromeDriver(CreateChromeOptions() as ChromeOptions),
                BrowserType.Safari => new SafariDriver(CreateSafariOptions() as SafariOptions),
                BrowserType.Firefox => throw new NotImplementedException(),
                BrowserType.Remote => new RemoteWebDriver(new Uri("http://localhost:8085"), CreateChromeOptions()),
                _ => throw new NotImplementedException(),
            };
            ;
        }

        private DriverOptions CreateChromeOptions()
        {
            var options = new ChromeOptions();
            if (ConfigurationHelper.BrowserConfiguration.EnableMobile)
                options.EnableMobileEmulation(ConfigurationHelper.BrowserConfiguration.DeviceName);
            return options;
        }

        private DriverOptions CreateSafariOptions()
        {
            //safari options setup can be done here
            return new SafariOptions();
        }
    }
}