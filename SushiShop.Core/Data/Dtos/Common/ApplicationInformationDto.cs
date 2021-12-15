using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class ApplicationInformationDto
    {
        [JsonProperty("needUpdate")]
        public bool ShouldUpdate { get; set; }

        public string? Message { get; set; }

        public PlatformsDto? Platforms { get; set; }
    }
}