using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CouponFollowRecruitmentTask.Interfaces;

namespace CouponFollowRecruitmentTask.Pages.Mobile
{
    internal class SearchForCouponPage : ISearchForCoupon
    {
        private IWebDriver Driver;

        public SearchForCouponPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void SearchForCoupon(string couponDomain)
        {
            Driver.FindElement(By.CssSelector("button#openSearch")).Click();
            Driver.FindElement(By.CssSelector("input.mobile-search-input")).SendKeys(couponDomain);

            WebDriverWait webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            webDriverWait.Until(ExpectedConditions.ElementExists(By.CssSelector("li a[data-domain]")));
            Driver.FindElement(By.CssSelector("li a[data-domain]")).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
        }
    }
}
