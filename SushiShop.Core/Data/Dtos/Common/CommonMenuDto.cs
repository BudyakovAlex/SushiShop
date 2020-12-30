using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class CommonMenuDto
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("pagetitle")]
        public string? Title { get; set; }

        [JsonProperty("alias")]
        public string? Alias { get; set; }

        [JsonProperty("extraData")]
        public ExtraDataDto? ExtraData { get; set; }

        [JsonProperty("createdon")]
        public long CreatedOn { get; set; }

        [JsonProperty("editedon")]
        public long EditedOn { get; set; }
    }
}
