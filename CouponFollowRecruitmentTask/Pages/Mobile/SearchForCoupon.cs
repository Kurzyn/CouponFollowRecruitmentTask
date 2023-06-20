using CouponFollowRecruitmentTask.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

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
