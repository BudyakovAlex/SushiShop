using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Shops
{
    public class ShopDto
    {
        public long Id { get; set; }

        [JsonProperty("cordinates")]
        public CoordinatesDto? Coordinates { get; set; }

        [JsonProperty("timezone")]
        public string? TimeZone { get; set; }

        [JsonProperty("mondayMode")]
        public string? MondayWorkingTime { get; set; }

        [JsonProperty("tuesdayMode")]
        public string? TuesdayWorkingTime { get; set; }

        [JsonProperty("wednesdayMode")]
        public string? WednesdayWorkingTime { get; set; }

        [JsonProperty("fridayMode")]
        public string? FridayWorkingTime { get; set; }

        [JsonProperty("saturdayMode")]
        public string? SaturdayWorkingTime { get; set; }

        [JsonProperty("sundayMode")]
        public string? SundayWorkingTime { get; set; }

        [JsonProperty("thursdayMode")]
        public string? ThursdayWorkingTime { get; set; }

        public string? City { get; set; }

        public string? Phone { get; set; }

        [JsonProperty("tempTimeTable")]
        public string? TempTimeTable { get; set; }

        [JsonProperty("franchisePoint")]
        public bool IsFranchisePoint { get; set; }

        [JsonProperty("delivery")]
        public bool IsDelivery { get; set; }

        [JsonProperty("pizzaPoint")]
        public bool IsPizzaPoint { get; set; }

        [JsonProperty("thisOptionIsNotAvailableForOrder")]
        public bool IsOrderAvailable { get; set; }

        [JsonProperty("merchant")]
        public long? MerchantId { get; set; }

        [JsonProperty("sendOrderToRKeeper")]
        public bool CanSendOrderToRKeeper { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("howToComeIn")]
        public string? DriveWay { get; set; }

        public long AddressId { get; set; }

        [JsonProperty("deliveryZones")]
        public DeliveryZoneDto[]? DeliveryZones { get; set; }

        [JsonProperty("deliveryTime")]
        public long DeliveryTime { get; set; }

        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }

        [JsonProperty("longtitle")]
        public string? LongTitle { get; set; }

        [JsonProperty("parent")]
        public long ParentId { get; set; }

        [JsonProperty("worktable")]
        public ShopWorkingInfoDto? ShopWorkingInfo { get; set; }

        public string[]? Phones { get; set; }

        public MetroDto[]? Metro { get; set; }

        [JsonProperty("gallery")]
        public ImageInfoDto[]? Images { get; }
    }
}