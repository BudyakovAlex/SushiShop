using SushiShop.Core.Data.Models.Stickers;
using SushiShop.Core.Data.Models.Toppings;

namespace SushiShop.Core.Data.Models.Products
{
    public class ProductParameters
    {
        public ProductParameters(StickerParams[]? stickers,
                                 Topping[]? availableToppings,
                                 string? proteins,
                                 string? fats,
                                 string? carbons,
                                 string? calorificValue,
                                 string? weight,
                                 string? volume)
        {
            Stickers = stickers;
            AvailableToppings = availableToppings;
            Proteins = proteins;
            Fats = fats;
            Carbons = carbons;
            CalorificValue = calorificValue;
            Weight = weight;
            Volume = volume;
        }

        public StickerParams[]? Stickers { get; }
        public Topping[]? AvailableToppings { get; }
        public string? Proteins { get; }
        public string? Fats { get; }
        public string? Carbons { get; }
        public string? CalorificValue { get; }
        public string? Weight { get; }
        public string? Volume { get; }
    }
}
