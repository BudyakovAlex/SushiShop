namespace SushiShop.Core.Data.Dtos.Menu
{
    public class CategoryIconDto
    {
        public CategoryIconDto(string original, string jpg, string webp)
        {
            Original = original;
            Jpg = jpg;
            Webp = webp;
        }

        public string Original { get; }

        public string Jpg { get; }

        public string Webp { get; }
    }
}
