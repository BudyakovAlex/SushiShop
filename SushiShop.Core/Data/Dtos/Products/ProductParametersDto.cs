using SushiShop.Core.Data.Dtos.Toppings;
using System.Collections.Generic;

namespace SushiShop.Core.Data.Dtos.Products
{
    public class ProductParametersDto
    {
        public string[]? Stickers { get; set; }
        public Dictionary<string, ToppingDto>? AvailableToppings { get; set; }
        public string? Proteins { get; set; }
        public string? Fats { get; set; }
        public string? Carbons { get; set; }
        public string? CalorificValue { get; set; }
        public string? Weight { get; set; }
        public string? Volume { get; set; }
    }
}
