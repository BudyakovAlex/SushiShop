using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Pagination
{
    public class PaginationContainerDto<TDto>
    {
        [JsonProperty("currentLimit")]
        public int CurrentLimit { get; set; }

        [JsonProperty("currentOffset")]
        public int CurrentOffset { get; set; }

        [JsonProperty("totalCount")]
        public int TotalCount { get; set; }

        [JsonProperty("data")]
        public TDto[]? Data { get; set; }
    }
}