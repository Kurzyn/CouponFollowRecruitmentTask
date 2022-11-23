using Ninject.Activation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;

namespace CouponFollowRecruitmentTask.Infrastructure
{
    internal class WebDriverProvider : Provider<IWebDriver>
    {
        protected override IWebDriver CreateInstance(IContext context)
        {
            var options = CreateChromeOptions();
            return new ChromeDriver(options);
        }

        private ChromeOptions CreateChromeOptions()
        {
            var options = new ChromeOptions();
            //options.EnableMobileEmulation("iPhone 12 Pro");
            return options;
        }
    }
}