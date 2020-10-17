namespace SushiShop.Core.Data.Models
{
    public class Currency
    {
        public Currency(string? code,
            string? symbol,
            int decimals)
        {
            Code = code;
            Symbol = symbol;
            Decimals = decimals;
        }

        public string? Code { get; }
        public string? Symbol { get; }
        public int Decimals { get; }
    }
}