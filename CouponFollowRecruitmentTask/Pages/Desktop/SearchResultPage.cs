using CouponFollowRecruitmentTask.Infrastructure;
using CouponFollowRecruitmentTask.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouponFollowRecruitmentTask.Pages.Desktop
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
            var deals = Driver.FindElements(By.CssSelector("div.deal a.couponUrl"));
            deals.First().Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }
    }
}
