using SushiShop.Core.Data.Dtos.Common;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Mappers
{
    public static class CurrencyMapper
    {
        public static Currency Map(this CurrencyDto currencyDto)
        {
            return new Currency(
                currencyDto.Code,
                currencyDto.Symbol,
                currencyDto.Decimals);
        }
    }
}