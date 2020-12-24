using Newtonsoft.Json;
using System;

namespace SushiShop.Core.Data.Dtos.Common
{
    public class ContentDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string? Title { get; set; }

        [JsonProperty("alias")]
        public string? Alias { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("createdon")]
        public DateTime? CreatedOn { get; set; }

        [JsonProperty("editedon")]
        public DateTime? EditedOn { get; set; }
    }
}