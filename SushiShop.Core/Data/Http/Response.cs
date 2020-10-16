namespace SushiShop.Core.Data.Http
{
    public class Response
    {
        public Response(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public bool IsSuccessful { get; }
    }

    public class Response<TData> : Response
    {
        public Response(bool isSuccessful, TData data)
            : base(isSuccessful)
        {
            Data = data;
        }

        public TData Data { get; }
    }
}
