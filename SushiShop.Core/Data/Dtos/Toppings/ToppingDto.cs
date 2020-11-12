using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Toppings
{
    public class ToppingDto
    {
        public int Id { get; set; }
        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }
        public long Price { get; set; }
        [JsonProperty("toppingCategory")]
        public string? ToppingCategory { get; set; }
        public int CountInBasket { get; set; }
    }
}
