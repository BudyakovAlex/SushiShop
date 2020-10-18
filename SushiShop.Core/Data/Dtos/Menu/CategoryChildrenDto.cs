namespace SushiShop.Core.Data.Dtos.Menu
{
    public class CategoryChildrenDto
    {
        public int[]? Ids { get; set; }
        public CategoryDto[]? SubCategories { get; set; }
    }
}
