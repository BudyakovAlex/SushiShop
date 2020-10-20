namespace SushiShop.Core.Data.Models
{
    public class Job
    {
        public Job(string url, string title, string text)
        {
            Url = url;
            Title = title;
            Text = text;
        }

        public string Url { get; }
        public string Title { get; }
        public string Text { get; }
    }
}