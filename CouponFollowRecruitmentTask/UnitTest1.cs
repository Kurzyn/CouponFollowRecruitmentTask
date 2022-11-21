
//"IAP" parallelization added thru anotation due to lack of support of .runsettings on Mac and in Visual Studio Code
//https://learn.microsoft.com/en-us/visualstudio/test/configure-unit-tests-by-using-a-dot-runsettings-file?view=vs-2022
//this feature can potentialy be marked as included only for specific builds
[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace CouponFollowRecruitmentTask
{
    [TestClass]
    public class UnitTest1 : BaseFixture
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}