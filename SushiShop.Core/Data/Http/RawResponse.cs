using Newtonsoft.Json;

namespace SushiShop.Core.Data.Http
{
    public class RawResponse
    {
        public RawResponse(bool isSuccessful, string[]? errors)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
        }

        [JsonProperty("success")]
        public bool IsSuccessful { get; }

        public string[]? Errors { get; }
    }

    public class RawResponse<TData> : RawResponse
        where TData : class
    {
        public RawResponse(bool isSuccessful, string[]? errors, TData? data)
            : base(isSuccessful, errors)
        {
            Data = data;
        }

        [JsonProperty("successData")]
        public TData? Data { get; }
    }
}
