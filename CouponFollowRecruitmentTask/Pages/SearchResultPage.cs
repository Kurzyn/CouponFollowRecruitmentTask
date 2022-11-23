using CouponFollowRecruitmentTask.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponFollowRecruitmentTask.Pages
{
    internal class SearchResultPage
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
            IReadOnlyCollection<IWebElement> deals;
            if (ConfigurationHelper.BrowserConfiguration.EnableMobile)
            {
                deals = Driver.FindElements(By.CssSelector("div.deal a.couponUrl"));
            }
            else
                deals = Driver.FindElements(By.CssSelector("section article.type-deal"));
            deals.First().Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }
    }
}
