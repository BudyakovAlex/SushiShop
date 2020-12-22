using System;
using SushiShop.Core.Data.Dtos.Profile;
using SushiShop.Core.Data.Models.Profile;

namespace SushiShop.Core.Mappers.Profile
{
    public static class TokenMapper
    {
        public static Token Map(this TokenDto dto) =>
            new Token(
                dto.Token!,
                dto.Header!,
                dto.HeaderPreffix!,
                DateTimeOffset.FromUnixTimeSeconds(dto.ExpiresAt));
    }
}
