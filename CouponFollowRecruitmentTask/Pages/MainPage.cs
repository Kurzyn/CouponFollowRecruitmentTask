using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CouponFollowRecruitmentTask.Pages
{

    public class MainPage
    {
        private readonly string dataDomain = "data-domain";
        private By slideElement = By.CssSelector("div[class*=swiper-slide-]");
        private By activeElement = By.CssSelector("div[class*=swiper-slide-active]");
        private By prevElement = By.CssSelector("div[class*=swiper-slide-prev]");
        private By nextElement = By.CssSelector("div[class*=swiper-slide-next]");
        private By todayTrendingDeals = By.CssSelector("section[class*=trending-deals] article");
        private By couponTitleLocator = By.CssSelector("p.title");

        private IReadOnlyCollection<IWebElement> SlideElements => Driver.FindElements(slideElement);
        private IReadOnlyCollection<IWebElement> TrendingDealsElements => Driver.FindElements(todayTrendingDeals);
        private IReadOnlyCollection<IWebElement> StaffPicksTitleElements => Driver.FindElements(By.CssSelector("div[class=staff-pick] a p.title"));
        private IReadOnlyCollection<IWebElement> StaffPicksDiv => Driver.FindElements(By.CssSelector("div[class='staff-pick'] a"));

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
            return elements.Select(x => x.GetDomAttribute(dataDomain)).ToList();
        }

        internal IEnumerable<double> GetPercentElementsValuesOnStaffPicks()
        {
            List<double> parsed = new();
            var searchPattern = @"\S?\d+(.\d+)?";

            var textValues = StaffPicksTitleElements.Select(x => x.Text.ToLowerInvariant());
            var percentsValues = textValues.Where(x => Regex.IsMatch(x, @"[%]"));
            var cleanedUpValues = percentsValues.Select(x => x.Replace("save", string.Empty).Replace("off", string.Empty).Trim());
            foreach(var x in cleanedUpValues)
            {
                if (!double.TryParse(Regex.Match(x, searchPattern).Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double percetnage))
                    throw new Exception($"Value on coupon is '{x}' and it's wrong for percetage type discount");
                parsed.Add(percetnage);
            }
            return parsed;
        }

        private Dictionary<string, string> GetStaffPicksDetails()
        {
            Dictionary<string, string> staffPicksDetails = new Dictionary<string, string>();
            foreach (var staffPick in StaffPicksDiv)
            {
                var couponTitle = staffPick.FindElement(couponTitleLocator);
                var couponDomain = staffPick.GetDomAttribute(dataDomain);
                var titleText = couponTitle.Text.ToLower();
                if (string.IsNullOrEmpty(titleText))
                    throw new Exception($"Coupon for {couponDomain} has missing title");
                staffPicksDetails.Add(couponDomain, titleText);
            }
            return staffPicksDetails;
        }

        internal Dictionary<string, double> GetMonetaryElementsValuesOnStaffPicks()
        {
            Dictionary<string, double> results = new Dictionary<string, double>();

            var details = GetStaffPicksDetails();
            foreach (var monetaryElement in details)
            {
                //assumption that monetary value will always have dolar sign in tittle(what if it will be false),
                //maybe should verify based on element type
                //maybe some functionality is covered by unit-tests?
                if (Regex.IsMatch(monetaryElement.Value, @"[$]"))
                {
                    double parsed;
                    var titleText = monetaryElement.Value
                        .Replace("$", string.Empty)
                        .Replace("take", string.Empty)
                        .Replace("off", string.Empty).Trim();

                    if (!double.TryParse(titleText, NumberStyles.Any, CultureInfo.InvariantCulture, out parsed))
                        throw new Exception($"Value on coupon for '{monetaryElement.Key}' has wrong value {titleText}");
                    results.Add(monetaryElement.Key, parsed);
                }
            }

            return results;
        }

        public IReadOnlyCollection<IWebElement> GetTodayTrendingDeals() => TrendingDealsElements;

        internal Dictionary<string, string> GetTextElemenetsValuesOnStaffPicks()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            var details = GetStaffPicksDetails();
            foreach (var titleElement in details)
            {
                if (!Regex.IsMatch(titleElement.Value, @"[$]") && !Regex.IsMatch(titleElement.Value, @"%"))
                {
                    results.Add(titleElement.Key, titleElement.Value);
                }
            }

            return results;
        }

        internal Dictionary<string, string> GetNotMatchingCoupons()
        {
            Dictionary<string, string> results = new Dictionary<string, string>();

            var details = GetStaffPicksDetails();
            foreach (var titleElement in details)
            {
                //assumptions based on coupons found on main page, maybe on Staff Picks they are presented in different way
                //need to verify with User Story of that feature
                if (!Regex.IsMatch(titleElement.Value, @"[$]") && !Regex.IsMatch(titleElement.Value, @"%")
                    && !titleElement.Value.Contains("save") && !titleElement.Value.Contains("free"))
                {
                    results.Add(titleElement.Key, titleElement.Value);
                }
            }
            return results;
        }

        internal string SearchForExisitingStore()
        {
            var choosenOne = StaffPicksDiv.First();
            var domain = choosenOne.GetDomAttribute(dataDomain);

            choosenOne.Click();
            
            WebDriverWait webDriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            webDriverWait.Until(drv => ((IJavaScriptExecutor)drv).ExecuteScript("return document.readyState").Equals("complete"));
            Driver.SwitchTo().Window(Driver.WindowHandles.Last());
            return domain; 
        }

        internal string GetCurrentUrl() => Driver.Url;
    }
}
