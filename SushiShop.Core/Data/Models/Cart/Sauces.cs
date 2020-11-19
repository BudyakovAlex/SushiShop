using SushiShop.Core.Data.Models.Common;
using System;

namespace SushiShop.Core.Data.Models.Cart
{
    public class Sauces
    {
        public Sauces(
            int id,
            string? pageTitle,
            string? alias,
            long parent,
            string? toppingCategory,
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
            this.Id = id;
            this.PageTitle = pageTitle;
            this.Alias = alias;
            this.Parent = parent;
            this.ToppingCategory = toppingCategory;
            this.PublishDate = publishDate;
            this.UnpublishDate = unpublishDate;
            this.IntroText = introText;
            this.CreationDate = creationDate;
            this.CountInBasket = countInBasket;
            this.Price = price;
            this.OldPrice = oldPrice;
            this.Currency = currency;
            this.MainImageInfo = mainImageInfo;
            this.OptionalImageInfo = optionalImageInfo;
        }

        public int Id { get;  }
        public string? PageTitle { get;  }
        public string? Alias { get;  }
        public long Parent { get;  }
        public string? ToppingCategory { get;  }
        public DateTime PublishDate { get;  }
        public DateTime UnpublishDate { get;  }
        public string? IntroText { get;  }
        public DateTime CreationDate { get;  }
        public string? PriceGroup { get;  }
        public int CountInBasket { get;  }
        public long Price { get;  }
        public long OldPrice { get;  }
        public Currency? Currency { get;  }
        public ImageInfo? MainImageInfo { get;  }
        public ImageInfo? OptionalImageInfo { get;  }
    }
}
