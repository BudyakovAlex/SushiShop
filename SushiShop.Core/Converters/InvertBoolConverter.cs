namespace SushiShop.Core.Converters
{
    public class InvertBoolConverter : BoolToValueConverter<bool>
    {
        public InvertBoolConverter() : base (false, true)
        {
        }
    }
}