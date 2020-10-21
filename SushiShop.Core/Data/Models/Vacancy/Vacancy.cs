namespace SushiShop.Core.Data.Models.Vacancy
{
    public class Vacancy
    {
        public Vacancy(string? url, string? title, string? text)
        {
            Url = url;
            Title = title;
            Text = text;
        }

        public string? Url { get; }
        public string? Title { get; }
        public string? Text { get; }
    }
}