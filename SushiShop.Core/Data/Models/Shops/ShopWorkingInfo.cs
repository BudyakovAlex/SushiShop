using System;
namespace SushiShop.Core.Data.Models.Shops
{
    public class ShopWorkingInfo
    {
        public ShopWorkingInfo(
            string availabilityStatus,
            bool isAvailable,
            string availabilityColor,
            DayOfWeek dayOfWeek,
            string? deliveryStatus,
            string? pickUpStatus)
        {
            AvailabilityStatus = availabilityStatus;
            IsAvailable = isAvailable;
            AvailabilityColor = availabilityColor;
            DayOfWeek = dayOfWeek;
            DeliveryStatus = deliveryStatus;
            PickUpStatus = pickUpStatus;
        }

        public string AvailabilityStatus { get; }

        public bool IsAvailable { get; set; }

        public string AvailabilityColor { get; }

        public DayOfWeek DayOfWeek { get; set; }

        public string? DeliveryStatus { get; }

        public string? PickUpStatus { get; }
    }
}
