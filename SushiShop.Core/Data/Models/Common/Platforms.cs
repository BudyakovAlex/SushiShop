namespace SushiShop.Core.Data.Models.Common
{
    public class Platforms
    {
        public Platforms(PlatformInformation ios, PlatformInformation android)
        {
            Ios = ios;
            Android = android;
        }

        public PlatformInformation Ios { get; }

        public PlatformInformation Android { get; }
    }
}
