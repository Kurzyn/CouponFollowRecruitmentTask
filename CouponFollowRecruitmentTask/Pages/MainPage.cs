using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouponFollowRecruitmentTask.Pages
{
    
    public class MainPage
    {
        private By slideElement = By.CssSelector("div[class*=swiper-slide-]");
        private By activeElement = By.CssSelector("div[class*=swiper-slide-active]");
        private By prevElement = By.CssSelector("div[class*=swiper-slide-prev]");
        private By nextElement = By.CssSelector("div[class*=swiper-slide-next]");
        private By todayTrendingDeals = By.CssSelector("section[class*=trending-deals] article");

        private IReadOnlyCollection<IWebElement> SlideElements => Driver.FindElements(slideElement);
        private IReadOnlyCollection<IWebElement> TrendingDealsElements => Driver.FindElements(todayTrendingDeals);
        
        private IWebDriver Driver { get; }
        
        public MainPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void OpenMainPage()
        {
            Driver.Navigate().GoToUrl("https://couponfollow.com");
        }

        public IReadOnlyCollection<IWebElement> GetCouponsFromCarusel() => SlideElements;

        public IReadOnlyCollection<IWebElement> GetTodayTrendingDeals() => TrendingDealsElements;


    }
}
