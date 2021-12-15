namespace SushiShop.Core.Data.Models.Common
{
    public class ApplicationInformation
    {
        public ApplicationInformation(bool shouldUpdate,
            string message,
            Platforms platforms)
        {
            ShouldUpdate = shouldUpdate;
            Message = message;
            Platforms = platforms;
        }

        public bool ShouldUpdate { get; }

        public string Message { get; }

        public Platforms Platforms { get; }
    }
}