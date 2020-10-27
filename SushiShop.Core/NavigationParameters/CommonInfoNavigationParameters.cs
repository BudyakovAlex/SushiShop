using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.NavigationParameters
{
    public class CommonInfoNavigationParameters
    {
        public CommonInfoNavigationParameters(CommonInfoType commonInfoType, string? city)
        {
            CommonInfoType = commonInfoType;
            City = city;
        }

        public CommonInfoType CommonInfoType { get; }

        public string? City { get; }
    }
}