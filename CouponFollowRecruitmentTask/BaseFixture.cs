using Ninject;
using OpenQA.Selenium;

namespace CouponFollowRecruitmentTask
{
    public abstract class BaseFixture
    {
        public IKernel kernel = new StandardKernel();

        [TestCleanup]
        public void CleanUp()
        {
            var driver = kernel.Get<IWebDriver>();
            driver?.Quit();
            kernel.Dispose();
        }

    }
}