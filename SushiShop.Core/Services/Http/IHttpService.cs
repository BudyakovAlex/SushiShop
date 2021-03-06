using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http
{
    public interface IHttpService
    {
        Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, string content, CancellationToken cancellationToken)
            where T : class;

        Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, CancellationToken cancellationToken)
            where T : class;

        Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, object body, CancellationToken cancellationToken)
            where T : class;

        Task<HttpResponse<T>> ExecuteMultipartAsync<T>(Method method, string url, object body, string[] files, CancellationToken cancellationToken)
            where T : class;

        Task<HttpResponse> ExecuteAsync(Method method, string url, string content, CancellationToken cancellationToken);

        Task<HttpResponse> ExecuteAsync(Method method, string url, CancellationToken cancellationToken);
    }
}
