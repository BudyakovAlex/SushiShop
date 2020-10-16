namespace SushiShop.Core.Data.Models.Menu
{
    public class CategoryChildren
    {
        public CategoryChildren(int[] ids, Category[] subCategories)
        {
            Ids = ids;
            SubCategories = subCategories;
        }

        public int[] Ids { get; }

        public Category[] SubCategories { get; }
    }
}
