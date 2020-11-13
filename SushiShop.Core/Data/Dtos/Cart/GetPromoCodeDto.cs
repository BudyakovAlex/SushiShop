using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Cart
{
    public class GetPromoCodeDto
    {
        [JsonProperty("basketId")]
        public Guid BaseketId { get; set; }

        [JsonProperty("city")]
        public string? City { get; set; }

        [JsonProperty("promocode")]
        public string? Promocode { get; set; }
    }
}
