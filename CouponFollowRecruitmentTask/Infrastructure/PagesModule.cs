using Ninject.Modules;
using OpenQA.Selenium;

using Mobile = CouponFollowRecruitmentTask.Pages.Mobile;
using Desktop = CouponFollowRecruitmentTask.Pages.Desktop;
using CouponFollowRecruitmentTask.Interfaces;

namespace CouponFollowRecruitmentTask.Infrastructure
{
    internal class PagesModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ISearchForCoupon>().To<Mobile.SearchForCouponPage>()
                .When(x => ConfigurationHelper.BrowserConfiguration.EnableMobile);
            Bind<ISearchForCoupon>().To<Desktop.SearchForCouponPage>()
                .When(x => !ConfigurationHelper.BrowserConfiguration.EnableMobile);
            Bind<ISearchResultPage>().To<Mobile.SearchResultPage>()
                .When(x => ConfigurationHelper.BrowserConfiguration.EnableMobile);
            Bind<ISearchResultPage>().To<Desktop.SearchResultPage>()
                .When(x => !ConfigurationHelper.BrowserConfiguration.EnableMobile);
        }
    }
}
