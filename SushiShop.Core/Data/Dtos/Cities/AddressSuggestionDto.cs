using SushiShop.Core.Data.Dtos.Common;
using System;
namespace SushiShop.Core.Data.Dtos.Cities
{
    public class AddressSuggestionDto
    {
        public string? Address { get; set; }

        public string? FullAddress { get; set; }

        public Guid FiasId { get; set; }

        public string? KladrId { get; set; }

        public long ZipCode { get; set; }

        public bool IsHouseAddress { get; set; }

        public CoordinatesDto? Coordinates { get; set; }
    }
}