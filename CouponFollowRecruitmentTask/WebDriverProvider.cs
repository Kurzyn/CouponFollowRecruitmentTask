using Ninject.Activation;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CouponFollowRecruitmentTask
{
    internal class WebDriverProvider : Provider<IWebDriver>
    {
        protected override IWebDriver CreateInstance(IContext context)
        {
            return new ChromeDriver();
        }
    }
}