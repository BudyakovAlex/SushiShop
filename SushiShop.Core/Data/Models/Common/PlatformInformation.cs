namespace SushiShop.Core.Data.Models.Common
{
    public class PlatformInformation
    {
        public PlatformInformation(
            string version,
            int build,
            string url)
        {
            Version = version;
            Build = build;
            Url = url;
        }

        public string Version { get; }

        public int Build { get; }

        public string Url { get; }
    }
}