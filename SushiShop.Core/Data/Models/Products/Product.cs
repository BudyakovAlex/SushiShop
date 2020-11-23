using System;
using SushiShop.Core.Data.Models.Common;

namespace SushiShop.Core.Data.Models.Products
{
    public class Product
    {
        public Product(
            long id,
            Guid? uid,
            string pageTitle,
            string? alias,
            long parent,
            DateTimeOffset publishDate,
            DateTimeOffset unpublishDate,
            string? introText,
            DateTimeOffset creationDate,
            ProductParameters? @params,
            string? priceGroup,
            Currency currency,
            long price,
            long oldPrice,
            ImageInfo? mainImageInfo,
            ImageInfo? optionalImageInfo,
            int countInBasket)
        {
            Id = id;
            Uid = uid;
            PageTitle = pageTitle;
            Alias = alias;
            Parent = parent;
            PublishDate = publishDate;
            UnpublishDate = unpublishDate;
            IntroText = introText;
            CreationDate = creationDate;
            Params = @params;
            PriceGroup = priceGroup;
            Currency = currency;
            Price = price;
            OldPrice = oldPrice;
            MainImageInfo = mainImageInfo;
            OptionalImageInfo = optionalImageInfo;
            CountInBasket = countInBasket;
        }

        public long Id { get; }
        public Guid? Uid { get; set; }
        public string PageTitle { get; }
        public string? Alias { get; }
        public long Parent { get; }
        public DateTimeOffset PublishDate { get; }
        public DateTimeOffset UnpublishDate { get; }
        public string? IntroText { get; }
        public DateTimeOffset CreationDate { get; }
        public ProductParameters? Params { get; }
        public string? PriceGroup { get; }
        public Currency Currency { get; }
        public long Price { get; }
        public long OldPrice { get; }
        public ImageInfo? MainImageInfo { get; }
        public ImageInfo? OptionalImageInfo { get; }
        public int CountInBasket { get; }
    }
}