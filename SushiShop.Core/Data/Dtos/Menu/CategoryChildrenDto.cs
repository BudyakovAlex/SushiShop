namespace SushiShop.Core.Data.Dtos.Menu
{
    public class CategoryChildrenDto
    {
        public CategoryChildrenDto(int[] ids, CategoryDto[] subCategories)
        {
            Ids = ids;
            SubCategories = subCategories;
        }

        public int[] Ids { get; }

        public CategoryDto[] SubCategories { get; }
    }
}
