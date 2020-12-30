using SushiShop.Core.Data.Enums;

namespace SushiShop.Core.Data.Models.Common
{
    public class ExtraData
    {
        public ExtraData(string? data, ExtraDataType type)
        {
            Data = data;
            Type = type;
        }

        public string? Data { get; }

        public ExtraDataType Type { get; }
    }
}