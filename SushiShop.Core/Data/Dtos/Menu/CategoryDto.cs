using Newtonsoft.Json;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Dtos.Menu
{
    public class CategoryDto
    {
        public long Id { get; set; }
        public string? PageTitle { get; set; }
        public string? LongTitle { get; set; }
        public string? Alias { get; set; }
        public string? IntroText { get; set; }
        public string? Content { get; set; }
        public int CreatedOn { get; set; }
        public int MenuIndex { get; set; }
        public int ItemsCount { get; set; }
        [JsonProperty("categotyIcon")]
        public ImageInfoDto? CategoryIcon { get; set; }
        public CategoryChildrenDto? Children { get; set; }
    }
}
