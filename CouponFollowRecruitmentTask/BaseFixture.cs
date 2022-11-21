using CouponFollowRecruitmentTask.Infrastructure;
using Ninject;
using OpenQA.Selenium;

namespace CouponFollowRecruitmentTask
{
    public abstract class BaseFixture
    {
        public IKernel kernel = new StandardKernel(new BrowserModule());

        [TestCleanup]
        public void CleanUp()
        {
            var driver = kernel.Get<IWebDriver>();
            driver?.Quit();
            kernel.Dispose();
        }

    }
}