using CouponFollowRecruitmentTask.Infrastructure;
using Ninject;
using OpenQA.Selenium;

namespace CouponFollowRecruitmentTask
{
    public abstract class BaseFixture
    {
        public IKernel kernel = new StandardKernel(new BrowserModule());

        [TestInitialize]
        public void InitPages()
        {
            kernel.Load(new PagesModule());
        }

        [TestCleanup]
        public void CleanUp()
        {
            var driver = kernel.Get<IWebDriver>();
            driver?.Quit();
            kernel.Dispose();
        }

    }
}