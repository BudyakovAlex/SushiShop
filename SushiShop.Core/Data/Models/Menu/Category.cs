using System;

namespace SushiShop.Core.Data.Models.Menu
{
    public class Category
    {
        public Category(
            int id,
            string pageTitle,
            string longTitle,
            string alias,
            string introText,
            string content,
            DateTimeOffset createdOn,
            int menuIndex,
            int itemsCount,
            ImageInfo? categoryIcon,
            CategoryChildren? children)
        {
            Id = id;
            PageTitle = pageTitle;
            LongTitle = longTitle;
            Alias = alias;
            IntroText = introText;
            Content = content;
            CreatedOn = createdOn;
            MenuIndex = menuIndex;
            ItemsCount = itemsCount;
            CategoryIcon = categoryIcon;
            Children = children;
        }

        public int Id { get; }

        public string PageTitle { get; }

        public string LongTitle { get; }

        public string Alias { get; }

        public string IntroText { get; }

        public string Content { get; }

        public DateTimeOffset CreatedOn { get; }

        public int MenuIndex { get; }

        public int ItemsCount { get; }

        public ImageInfo? CategoryIcon { get; }

        public CategoryChildren? Children { get; }
    }
}