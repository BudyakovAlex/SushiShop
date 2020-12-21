﻿using SushiShop.Core.Data.Dtos.Menu;
using SushiShop.Core.Data.Models.Menu;
using SushiShop.Core.Mappers.Common;
using System;

namespace SushiShop.Core.Mappers.Menu
{
    public static class CategoryMapper
    {
        public static Category Map(this CategoryDto dto)
        {
            return new Category(
                dto.Id,
                dto.PageTitle!,
                dto.LongTitle!,
                dto.Alias!,
                dto.IntroText!,
                dto.Content!,
                DateTimeOffset.FromUnixTimeSeconds(dto.CreatedOn),
                dto.MenuIndex,
                dto.ItemsCount,
                dto.CategoryIcon?.Map(),
                dto.Children?.Map());
        }
    }
}