using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Data.Http;

namespace SushiShop.Core.Services.Http
{
    public class HttpService : IHttpService
    {
        private const string BaseUrl = "https://sushishop.ru/api/";

        private readonly HttpClient client;

        public HttpService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.Timeout = TimeSpan.FromMinutes(5);
        }

        public Task<HttpResponse> PostAsync(string url, string content, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(content)
            };

            return ExecuteAsync(request, cancellationToken);
        }

        public Task<HttpResponse> PostAsync(string url, CancellationToken cancellationToken) =>
            ExecuteAsync(new HttpRequestMessage(HttpMethod.Post, url), cancellationToken);

        private async Task<HttpResponse> ExecuteAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                using var response = await client.SendAsync(request, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return HttpResponse.Success(data, response.StatusCode);
                }
                else
                {
                    return HttpResponse.Error(exception: null, response.StatusCode);
                }
            }
            // There's a bug in the library, in case of a timeout, TaskCanceledException is thrown instead of TimeoutException.
            // https://stackoverflow.com/questions/10547895/how-can-i-tell-when-httpclient-has-timed-out/19822695#19822695
            catch (TaskCanceledException taskCanceledException)
                when (taskCanceledException.CancellationToken != cancellationToken)
            {
                return HttpResponse.TimedOut();
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            // Just in case the above bug is fixed.
            catch (TimeoutException)
            {
                return HttpResponse.TimedOut();
            }
            catch (Exception exception)
            {
                return HttpResponse.Error(exception, statusCode: null);
            }
        }
    }
}
