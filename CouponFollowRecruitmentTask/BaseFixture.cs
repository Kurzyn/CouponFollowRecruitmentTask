using Ninject;

namespace CouponFollowRecruitmentTask
{
    public abstract class BaseFixture
    {
        public IKernel kernel = new StandardKernel();

    }
}