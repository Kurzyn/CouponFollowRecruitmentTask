namespace CouponFollowRecruitmentTask.Infrastructure
{
    public sealed class BrowserConfiguration
    {
        public BrowserType BrowserType { get; set; }
        public bool EnableMobile { get; set; }
        public string? EnabledDevice { get; set; }
    }
}
