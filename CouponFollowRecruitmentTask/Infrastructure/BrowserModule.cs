using Ninject.Modules;
using OpenQA.Selenium;

namespace CouponFollowRecruitmentTask.Infrastructure
{
    internal class BrowserModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWebDriver>().ToProvider<WebDriverProvider>().InThreadScope();
        }
    }
}
