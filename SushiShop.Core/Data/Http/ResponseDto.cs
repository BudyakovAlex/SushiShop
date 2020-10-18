using Newtonsoft.Json;

namespace SushiShop.Core.Data.Http
{
    public class ResponseDto
    {
        public ResponseDto(bool isSuccessful, string[]? errors)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
        }

        [JsonProperty("success")]
        public bool IsSuccessful { get; }

        public string[]? Errors { get; }
    }

    public class ResponseDto<TData> : ResponseDto
        where TData : class
    {
        public ResponseDto(bool isSuccessful, string[]? errors, TData? successData)
            : base(isSuccessful, errors)
        {
            SuccessData = successData;
        }

        public TData? SuccessData { get; }
    }
}
