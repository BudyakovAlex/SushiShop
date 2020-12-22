using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http
{
    public interface IHttpService
    {
        Task<HttpResponse<T>> ExecuteAnonymouslyAsync<T>(Method method, string url, object? content, CancellationToken cancellationToken)
            where T : class;

        Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, object? content, CancellationToken cancellationToken)
            where T : class;

        Task<HttpResponse<T>> ExecuteMultipartAsync<T>(Method method, string url, object body, string[] files, CancellationToken cancellationToken)
            where T : class;
    }
}
