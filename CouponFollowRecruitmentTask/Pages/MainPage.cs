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
        private IWebDriver Driver { get; }
        
        public MainPage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void OpenMainPage()
        {
            Driver.Navigate().GoToUrl("https://couponfollow.com");
        }
    }
}
