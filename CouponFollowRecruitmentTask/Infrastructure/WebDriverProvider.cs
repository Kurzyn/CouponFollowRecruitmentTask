using Ninject.Activation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;

namespace CouponFollowRecruitmentTask.Infrastructure
{
    internal class WebDriverProvider : Provider<IWebDriver>
    {

        protected override IWebDriver CreateInstance(IContext context)
        {
            switch (ConfigurationHelper.BrowserConfiguration.BrowserType)
            {
                case BrowserType.Chrome:
                    return new ChromeDriver(CreateChromeOptions());
                case BrowserType.Safari:
                    throw new NotImplementedException();
                case BrowserType.Firefox:
                    throw new NotImplementedException();
                default: throw new NotImplementedException();
            };
        }

        private ChromeOptions CreateChromeOptions()
        {
            var options = new ChromeOptions();
            if(ConfigurationHelper.BrowserConfiguration.EnableMobile)
                options.EnableMobileEmulation(ConfigurationHelper.BrowserConfiguration.DeviceName);
            return options;
        }

        private SafariOptions CreateSafariOptions()
        {
            //safari options setup can be done here
            return new SafariOptions();
        }
    }
}