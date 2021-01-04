namespace SushiShop.Core.Data.Models.Common
{
    public class Metro
    {
        public Metro(
            double distance,
            string? line,
            string? name,
            string? measurement)
        {
            Distance = distance;
            Line = line;
            Name = name;
            Measurement = measurement;
        }

        public double Distance { get; }

        public string? Line { get; }

        public string? Name { get; }

        public string? Measurement { get; }
    }
}