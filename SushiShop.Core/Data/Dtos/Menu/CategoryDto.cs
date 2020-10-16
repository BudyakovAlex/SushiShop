namespace SushiShop.Core.Data.Dtos.Menu
{
    public class CategoryDto
    {
        public CategoryDto(
            int id,
            string pageTitle,
            string longTitle,
            string alias,
            string introText,
            string content,
            int createdOn,
            int menuIndex,
            int itemsCount,
            CategoryIconDto? categotyIcon,
            CategoryChildrenDto? childrens)
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
            CategotyIcon = categotyIcon;
            Childrens = childrens;
        }

        public int Id { get; }

        public string PageTitle { get; }

        public string LongTitle { get; }

        public string Alias { get; }

        public string IntroText { get; }

        public string Content { get; }

        public int CreatedOn { get; }

        public int MenuIndex { get; }

        public int ItemsCount { get; }

        public CategoryIconDto? CategotyIcon { get; }

        public CategoryChildrenDto? Childrens { get; }
    }
}
