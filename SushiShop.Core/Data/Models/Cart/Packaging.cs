﻿using System;
using SushiShop.Core.Data.Dtos.Common;

namespace SushiShop.Core.Data.Models.Cart
{
    public class Packaging
    {
        public Packaging(
            int id, string? pageTitle, string? alias, 
            long parent, DateTime publishDate, DateTime unpublishDate,
            string? introText, DateTime creationDate, int countInBasket, 
            long price, long oldPrice, CurrencyDto? currency, 
            ImageInfoDto? mainImageInfo, ImageInfoDto? optionalImageInfo)
        {
            Id = id;
            PageTitle = pageTitle;
            Alias = alias;
            Parent = parent;
            PublishDate = publishDate;
            UnpublishDate = unpublishDate;

            IntroText = introText;
            CreationDate = creationDate;
            CountInBasket = countInBasket;
            Price = price;
            OldPrice = oldPrice;
            Currency = currency;
            MainImageInfo = mainImageInfo;
            OptionalImageInfo = optionalImageInfo;
        }

        public int Id { get; set; }

        public string? PageTitle { get; set; }

        public string? Alias { get; set; }

        public long Parent { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UnpublishDate { get; set; }

        public string? IntroText { get; set; }

        public DateTime CreationDate { get; set; }

        public int CountInBasket { get; set; }

        public long Price { get; set; }

        public long OldPrice { get; set; }

        public CurrencyDto? Currency { get; set; }

        public ImageInfoDto? MainImageInfo { get; set; }

        public ImageInfoDto? OptionalImageInfo { get; set; }
    }
}
