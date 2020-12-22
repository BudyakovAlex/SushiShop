using System;
using Newtonsoft.Json;

namespace SushiShop.Core.Common
{
    public static class Json
    {
        public static string Serialize<T>(T value) =>
            JsonConvert.SerializeObject(value);

        public static T ForceDeserialize<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value);

        public static (T? data, Exception? exception) Deserialize<T>(string? value)
            where T : class
        {
            if (value is null)
            {
                return default;
            }

            try
            {
                var data = JsonConvert.DeserializeObject<T>(value);
                return (data, exception: null);
            }
            catch (Exception exception)
            {
                return (data: null, exception);
            }
        }
    }
}
