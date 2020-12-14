using SushiShop.Core.Data.Models.Orders;

namespace SushiShop.Core.Extensions
{
    public static class PickupPointExtensions
    {
        public static string GetPhonesStringPresentation(this PickupPoint pickupPoint)
        {
            return string.Join("\n", pickupPoint.Phones);
        }

        public static string GetWorkingTimeStringPresentation(this PickupPoint pickupPoint)
        {
            return string.Join("\n", pickupPoint.Worktime);
        }
    }
}
