using CouponFollowRecruitmentTask.Pages;
using CouponFollowRecruitmentTask.Pages.Mobile;
using FluentAssertions;
using Ninject;

//"IAP" parallelization added thru anotation due to lack of support of .runsettings on Mac and in Visual Studio Code
//https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022
//this feature can potentialy be marked as included only for specific builds
[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace CouponFollowRecruitmentTask
{
    [TestClass]
    public class MainPageTests : BaseFixture
    {
        MainPage mainPage;
        SearchResultPage searchResultPage;

        [TestInitialize]
        public void Init()
        {
            mainPage = kernel.Get<MainPage>();
            searchResultPage = kernel.Get<SearchResultPage>();
        }

        [TestMethod]
        [TestCategory("Test for task 1.")]
        public void ShouldHaveTodaysTopCouponElementsDisplayed()
        {
            mainPage.OpenMainPage();

            mainPage.GetCouponsCountFromCarusel().Should().BeOneOf(new[] { 3, 6, 9 });
           //mainPage.GetDisplayedCouponsFromCarusel();
        }

        [TestMethod]
        [TestCategory("Test for task 2.")]
        public void ShouldHaveAtLeast30TodayTopDealsOnMainPage()
        {
            mainPage.OpenMainPage();

            mainPage.GetTodayTrendingDeals().Should().HaveCountGreaterThanOrEqualTo(30, "there should be more trending coupons");
        }

        [TestMethod]
        [TestCategory("Test for task 3.")]
        [TestCategory("Staff Picks")]
        public void ShouldHaveUniqueElementsOnStaffPicksCoupons()
        {
            mainPage.OpenMainPage();
            mainPage.GetStaffPicksDomains().Should().OnlyHaveUniqueItems();
        }

        [TestMethod]
        [TestCategory("Test for task 3.")]
        [TestCategory("Staff Picks")]
        public void ShouldHaveProperDiscountValuesOnMonetaryStaffPicksCoupons()
        {
            //App/Testig environmet should be initialized with data for verification.
            //Right now tests will be failing if there is no Coupon with Monetary discount that goes for every coupon type
            mainPage.OpenMainPage();
            var coupons = mainPage.GetMonetaryElementsValuesOnStaffPicks();
            coupons.Should().NotBeNullOrEmpty();
            foreach (var coupon in coupons)
            {
                //assumtion that coupons are in such range - should confirm that
                coupon.Value.Should().BeInRange(1, 500, $"that's satisfactory range for {coupon.Key}");
            }
        }

        [TestMethod]
        [TestCategory("Test for task 3.")]
        [TestCategory("Staff Picks")]
        public void ShouldHaveProperDiscountValuesOnPercentageStaffPicksCoupons()
        {
            //App/Testig environmet should be initialized with data for verification.
            //Right now tests will be failing if there is no Coupon with Percentage discount that goes for every coupon type
            mainPage.OpenMainPage();
            foreach (double coupon in mainPage.GetPercentElementsValuesOnStaffPicks())
            {
                coupon.Should().BeInRange(1, 100);
            }
        }

        [TestMethod]
        [TestCategory("Test for task 3.")]
        [TestCategory("Staff Picks")]
        public void ShouldHaveProperDiscountValuesOnTextStaffPicksCoupons()
        {
            //App/Testig environmet should be initialized with data for verification.
            //Right now tests will be failing if there is no Coupon with Text discount that goes for every coupon type
            mainPage.OpenMainPage();
            mainPage.GetTextElemenetsValuesOnStaffPicks().Should().NotBeNullOrEmpty("there should be coupons with just text discount on Staff Picks");
        }

        [TestMethod]
        [TestCategory("Test for task 3.")]
        [TestCategory("Staff Picks")]
        public void ShouldHaveProperDiscounts()
        {
            //IF one knows that values are tested on lower level
            //and that there could not be issue with missing signes for '%' or for '$'
            //one could somewhat test that p.title text with such test

            mainPage.OpenMainPage();
            mainPage.GetNotMatchingCoupons().Should().BeNullOrEmpty("coupons should have title only matching assumptions");
        }

        [TestMethod]
        [TestCategory("Test for task 4.")]
        public void ShouldBeMovedToSiteAfterClickingCoupon()
        {
            mainPage.OpenMainPage();
            var couponDomain = mainPage.GetExisitingStoreFromStaffPicks();
            mainPage.SearchForCoupon(couponDomain);
            searchResultPage.OpenFirstResult();
            mainPage.GetCurrentUrl().Should().Contain(couponDomain);

        }
    }
}