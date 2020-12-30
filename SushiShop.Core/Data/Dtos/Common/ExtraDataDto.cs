using Newtonsoft.Json;
using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class ExtraDataDto
    {
        [JsonProperty("prop")]
        public string? Data { get; set; }

        public ExtraDataType Type { get; set; }
    }
}