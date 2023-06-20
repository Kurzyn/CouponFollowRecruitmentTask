using CouponFollowRecruitmentTask.Infrastructure;
using Ninject;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace CouponFollowRecruitmentTask
{
    public abstract class BaseFixture
    {
        public IKernel kernel = new StandardKernel(new BrowserModule());
        public TestContext TestContext { get; set; }


        [TestInitialize]
        public void InitPages()
        {
            kernel.Load(new PagesModule());
        }

        [TestCleanup]
        public void CleanUp()
        {
            var driver = kernel.Get<IWebDriver>();
            if (driver is not null && TestContext.CurrentTestOutcome != UnitTestOutcome.Passed)
            {
                var fileName = $"screenshot_{TestContext.TestName}_{DateTime.Now.ToString("dd_MM_YYYY_mm_ss")}.png";
                var screenshot = driver.TakeScreenshot();
                screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
                TestContext.AddResultFile(fileName);
            }
            driver?.Quit();
            kernel.Dispose();
        }

    }
}