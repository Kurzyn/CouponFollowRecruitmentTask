using CouponFollowRecruitmentTask.Pages;
using FluentAssertions;
using Ninject;

//"IAP" parallelization added thru anotation due to lack of support of .runsettings on Mac and in Visual Studio Code
//https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022
//this feature can potentialy be marked as included only for specific builds
[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace CouponFollowRecruitmentTask
{
    [TestClass]
    public class UnitTest1 : BaseFixture
    {
        MainPage mainPage;
        
        [TestInitialize]
        public void Init()
        {
            mainPage = kernel.Get<MainPage>();
        }

        [TestMethod]
        public void ShouldHaveTopDealsElementsDisplayed()
        {
            mainPage.OpenMainPage();

            mainPage.GetCouponsCountFromCarusel().Should().BeOneOf(new[] { 3, 6, 9 });
        }

        [TestMethod]
        public void ShouldHaveAtLease30TodayTopDealsOnMainPage()
        {
            mainPage.OpenMainPage();

            mainPage.GetTodayTrendingDeals().Should().HaveCountGreaterThanOrEqualTo(30, "there should be more trending coupons");
        }

        [TestMethod]
        [Ignore]
        public void ShouldHaveProperDiscountValuesOnStaffPicksCoupons()
        {
            throw new NotImplementedException();
        }
    }
}