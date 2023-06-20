using CouponFollowRecruitmentTask.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace CouponFollowRecruitmentTask.Pages.Desktop
{
    internal class SearchForCouponPage : ISearchForCoupon
    {
        private IWebElement SearchInput => Driver.FindElement(By.CssSelector("input[data-func=openSearch]"));

        private IWebDriver Driver { get; }

        public SearchForCouponPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void SearchForCoupon(string couponDomain)
        {
            SearchInput.Click();
            SearchInput.SendKeys(couponDomain);

            WebDriverWait webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            webDriverWait.Until(ExpectedConditions.ElementExists(By.CssSelector("li a[data-domain]")));
            Driver.FindElement(By.CssSelector("li a[data-domain]")).Click();
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            SearchInput.SendKeys(Keys.Enter);
        }
    }
}
