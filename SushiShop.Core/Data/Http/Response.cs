namespace SushiShop.Core.Data.Http
{
    public class Response
    {
        public Response(bool isSuccessful, params string[] errors)
        {
            IsSuccessful = isSuccessful;
            Errors = errors;
        }

        public bool IsSuccessful { get; }

        public string[] Errors { get; }
    }

    public class Response<TData> : Response
    {
        public Response(bool isSuccessful, TData data, params string[] errors)
            : base(isSuccessful, errors)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}