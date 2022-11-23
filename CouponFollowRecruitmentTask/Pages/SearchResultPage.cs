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
            //ifology
            var deals = Driver.FindElements(By.CssSelector("section article.type-deal"));
            //mobile var deals = Driver.FindElements(By.CssSelector("div.deal a.couponUrl"));
            deals.First().Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }
    }
}
