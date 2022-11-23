using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private IReadOnlyCollection<IWebElement> StaffPicksTitleElements => Driver.FindElements(By.CssSelector("div[class=staff-pick] a p.title"));

        private IWebDriver Driver { get; }

        public MainPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void OpenMainPage()
        {
            Driver.Navigate().GoToUrl("https://couponfollow.com");
        }

        public int GetCouponsCountFromCarusel()
        {
            return SlideElements.Select(x => x.GetDomAttribute("data-swiper-slide-index")).Distinct().Count();
        }

        internal void GetDisplayedCouponsFromCarusel()
        {
            throw new NotImplementedException();
        }

        internal List<string> GetStaffPicksDomains()
        {
            var elements = Driver.FindElements(By.CssSelector("div[class='staff-pick'] a"));
            //elements should be unique
            return elements.Select(x => x.GetDomAttribute("data-domain")).ToList();
        }

        internal IEnumerable<double> GetPercentElementsValuesOnStaffPicks()
        {
            var searchPattern = @"\S?\d+";

            var textValues = StaffPicksTitleElements.Select(x => x.Text.ToLowerInvariant());
            var percentsValues = textValues.Where(x => Regex.IsMatch(x, @"[%]"));
            var cleanedUpValues = percentsValues.Select(x => x.Replace("save", string.Empty).Replace("off", string.Empty).Trim());
            var parsed = cleanedUpValues.Select(x => double.Parse(Regex.Match(x, searchPattern).Value));

            return parsed;
        }

        internal IEnumerable<double> GetMonetaryElementsValuesOnStaffPicks()
        {
            List<double> results = new List<double>();

            var staffPicksDiv = Driver.FindElements(By.CssSelector("div[class='staff-pick'] a"));
            foreach (var staffPick in staffPicksDiv)
            {
                var couponTitle = staffPick.FindElement(By.CssSelector("p.title"));
                var couponDomain = staffPick.GetDomAttribute("data-domain");
                var titleText = couponTitle.Text.ToLower();
                if (string.IsNullOrEmpty(titleText))
                    throw new Exception($"Coupon for {couponDomain} has missing title");

                //assumption that monetary value will always have dolar sign in tittle(what if it will be false),
                //maybe should verify based on element type
                //maybe some functionality is covered by unit-tests?
                if (Regex.IsMatch(titleText, @"[$]"))
                {
                    double parsed;
                    titleText = titleText
                        .Replace("$", string.Empty)
                        .Replace("take", string.Empty)
                        .Replace("off", string.Empty).Trim();

                    if (!double.TryParse(titleText, out parsed))
                        throw new Exception($"Value on coupon for '{couponDomain}' has wrong value {titleText}");
                    results.Add(parsed);
                }
            }

            return results;
        }

        public IReadOnlyCollection<IWebElement> GetTodayTrendingDeals() => TrendingDealsElements;

        internal List<string> GetTextElemenetsValuesOnStaffPicks()
        {
            List<string> results = new List<string>();
            var staffPicksDiv = Driver.FindElements(By.CssSelector("div[class='staff-pick'] a"));

            foreach (var staffPick in staffPicksDiv)
            {
                var couponTitle = staffPick.FindElement(By.CssSelector("p.title"));
                var couponDomain = staffPick.GetDomAttribute("data-domain");
                var titleText = couponTitle.Text.ToLower();
                if (string.IsNullOrEmpty(titleText))
                    throw new Exception($"Coupon for {couponDomain} has missing title");
                if(!Regex.IsMatch(titleText, @"[$]") && !Regex.IsMatch(titleText, @"%"))
                {
                    results.Add(titleText);
                }
            }

            return results;
        }

        internal List<string> GetNotMatchingCoupons()
        {
            List<string> results = new List<string>();
            var staffPicksDiv = Driver.FindElements(By.CssSelector("div[class='staff-pick'] a"));

            foreach (var staffPick in staffPicksDiv)
            {
                var couponTitle = staffPick.FindElement(By.CssSelector("p.title"));
                var couponDomain = staffPick.GetDomAttribute("data-domain");
                var titleText = couponTitle.Text.ToLower();
                if (string.IsNullOrEmpty(titleText))
                    throw new Exception($"Coupon for {couponDomain} has missing title");

                //assumptions based on coupons found on main page, maybe on Staff Picks ther are presented in different way
                //need to verify with User Story of that feature
                if (!Regex.IsMatch(titleText, @"[$]") && !Regex.IsMatch(titleText, @"%")
                    && !titleText.Contains("save") && !titleText.Contains("free"))
                {
                    results.Add(titleText);
                }
            }
            return results;
        }
    }
}
