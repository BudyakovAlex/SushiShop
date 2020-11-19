﻿using System;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Cart
{
    public class Packaging
    {
        public Packaging(
            long id, 
            string? pageTitle, 
            string? alias,
            long parent, 
            DateTime publishDate, 
            DateTime unpublishDate,
            string? introText, 
            DateTime creationDate, 
            int countInBasket,
            long price, 
            long oldPrice, 
            Currency? currency,
            ImageInfo? mainImageInfo,
            ImageInfo? optionalImageInfo)
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

        public long Id { get; set; }

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

        public Currency? Currency { get; set; }

        public ImageInfo? MainImageInfo { get; set; }

        public ImageInfo? OptionalImageInfo { get; set; }
    }
}
