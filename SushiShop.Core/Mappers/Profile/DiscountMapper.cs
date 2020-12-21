using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace SushiShop.Core.Mappers.Profile
{
    public static class DiscountMapper
    {
        public static ProfileDiscount Map(this ProfileDiscountDto profileDiscountDto)
        {
            return new ProfileDiscount(profileDiscountDto.Title,
                                       profileDiscountDto.Phone,
                                       profileDiscountDto.BalanceToNextLevel,
                                       profileDiscountDto.Discount,
                                       profileDiscountDto.Certificate,
                                       profileDiscountDto.Bonuses);
        }
    }
}
