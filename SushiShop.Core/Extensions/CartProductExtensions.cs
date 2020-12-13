using SushiShop.Core.Data.Models.Cart;

namespace SushiShop.Core.Extensions
{
    public static class CartProductExtensions
    {
        public static string GetValue(this CartProduct product)
        {
            if (product.Weight != null)
            {
                return product.Weight;
            }

            if (product.Volume != null)
            {
                return product.Volume;
            }

            return string.Empty;
        }
    }
}
