using System;
using SushiShop.Core.Data.Models.Menu;

namespace SushiShop.Core.Data.Models.Promotions
{
    public class Promotion
    {
        public Promotion(
            int id,
            string pageTitle,
            string longTitle,
            string alias,
            string introText,
            string content,
            DateTimeOffset publicationStartDate,
            DateTimeOffset publicationEndDate,
            DateTimeOffset createdOn,
            string url,
            string? showOnHome,
            ImageInfo squareImageInfo,
            ImageInfo rectangularImageInfo,
            string[] cities,
            object? productId)
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
            ProductId = productId;
        }

        public int Id { get; }
        public string PageTitle { get; }
        public string LongTitle { get; }
        public string Alias { get; }
        public string IntroText { get; }
        public string Content { get; }
        public DateTimeOffset PublicationStartDate { get; }
        public DateTimeOffset PublicationEndDate { get; }
        public DateTimeOffset CreatedOn { get; }
        public string Url { get; }
        public string? ShowOnHome { get; }
        public ImageInfo SquareImageInfo { get; }
        public ImageInfo RectangularImageInfo { get; }
        public string[] Cities { get; }
        public object? ProductId { get; }
    }
}
