using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http
{
    public interface IHttpService
    {
        Task<HttpResponse> PostAsync(string url, string content, CancellationToken cancellationToken);

        Task<HttpResponse> PostAsync(string url, CancellationToken cancellationToken);
    }
}
