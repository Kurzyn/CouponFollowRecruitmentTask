using CouponFollowRecruitmentTask.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CouponFollowRecruitmentTask.Pages.Mobile
{
    internal class SearchResultPage : ISearchResultPage
    {
        private IWebDriver Driver { get; }

        public SearchResultPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void OpenFirstResult()
        {
            WebDriverWait webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            webDriverWait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").Equals("complete"));
            var deals = Driver.FindElements(By.CssSelector("section article.type-deal"));
            deals.First().Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }
    }
}
