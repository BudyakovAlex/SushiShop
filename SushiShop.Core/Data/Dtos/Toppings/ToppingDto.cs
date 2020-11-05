using Newtonsoft.Json;

namespace SushiShop.Core.Data.Dtos.Toppings
{
    public class ToppingDto
    {
        public long Id { get; set; }
        [JsonProperty("pagetitle")]
        public string? PageTitle { get; set; }
        public long Price { get; set; }
        public int CountInBasket { get; set; }
    }
}
