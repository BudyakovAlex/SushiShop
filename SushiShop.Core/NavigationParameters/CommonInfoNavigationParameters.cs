using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.NavigationParameters
{
    public class CommonInfoNavigationParameters
    {
        public CommonInfoNavigationParameters(Data.Enums.CommonInfoType commonInfoType, string? city)
        {
            CommonInfoType = commonInfoType;
            City = city;
        }

        public Data.Enums.CommonInfoType CommonInfoType { get; }

        public string? City { get; }
    }
}
