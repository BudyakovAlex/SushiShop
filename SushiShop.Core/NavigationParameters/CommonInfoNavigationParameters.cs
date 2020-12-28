using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.NavigationParameters
{
    public class CommonInfoNavigationParameters
    {
        public CommonInfoNavigationParameters(
            CommonInfoType commonInfoType,
            long? id,
            string? city,
            string? alias,
            string? title)
        {
            CommonInfoType = commonInfoType;
            Id = id;
            City = city;
            Alias = alias;
            Title = title;
        }

        public CommonInfoType CommonInfoType { get; }

        public long? Id { get; }

        public string? City { get; }

        public string? Alias { get; }

        public string? Title { get; }
    }
}