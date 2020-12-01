using System;
using SushiShop.Core.Data.Models.Common;
using SushiShop.Core.Data.Models.Products;

namespace SushiShop.Core.Data.Models.Promotions
{
    public class Promotion
    {
        public Promotion(
            long id,
            string pageTitle,
            string longTitle,
            string alias,
            string introText,
            string content,
            DateTimeOffset? publicationStartDate,
            DateTimeOffset? publicationEndDate,
            DateTimeOffset createdOn,
            string url,
            string? showOnHome,
            ImageInfo squareImageInfo,
            ImageInfo rectangularImageInfo,
            string[] cities,
            Product? product)
        {
            Id = id;
            PageTitle = pageTitle;
            LongTitle = longTitle;
            Alias = alias;
            IntroText = introText;
            Content = content;
            PublicationStartDate = publicationStartDate;
            PublicationEndDate = publicationEndDate;
            CreatedOn = createdOn;
            Url = url;
            ShowOnHome = showOnHome;
            SquareImageInfo = squareImageInfo;
            RectangularImageInfo = rectangularImageInfo;
            Cities = cities;
            Product = product;
        }

        public long Id { get; }
        public string PageTitle { get; }
        public string LongTitle { get; }
        public string Alias { get; }
        public string IntroText { get; }
        public string Content { get; }
        public DateTimeOffset? PublicationStartDate { get; }
        public DateTimeOffset? PublicationEndDate { get; }
        public DateTimeOffset CreatedOn { get; }
        public string Url { get; }
        public string? ShowOnHome { get; }
        public ImageInfo SquareImageInfo { get; }
        public ImageInfo RectangularImageInfo { get; }
        public string[] Cities { get; }
        public Product? Product { get; }
    }
}
