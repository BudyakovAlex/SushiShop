using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
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

        public async Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, string content, CancellationToken cancellationToken) where T : class
        {
            var response = await ExecuteAsync(method, url, content, cancellationToken);
            return Deserialize<T>(response);
        }

        public async Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, object body, CancellationToken cancellationToken) where T : class
        {
            var content = Json.Serialize(body);
            var response = await ExecuteAsync(method, url, content, cancellationToken);
            return Deserialize<T>(response);
        }

        public async Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, CancellationToken cancellationToken) where T : class
        {
            var response = await ExecuteAsync(method, url, cancellationToken);
            return Deserialize<T>(response);
        }

        public Task<HttpResponse> ExecuteAsync(Method method, string url, string content, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(ToHttpMethod(method), url)
            {
                Content = new StringContent(content)
            };

            return ExecuteAsync(request, cancellationToken);
        }

        public Task<HttpResponse> ExecuteAsync(Method method, string url, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(ToHttpMethod(method), url);
            return ExecuteAsync(request, cancellationToken);
        }

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

                return HttpResponse.Error(exception: null, response.StatusCode);
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

        private HttpResponse<T> Deserialize<T>(HttpResponse response)
            where T : class
        {
            switch (response.ResponseStatus)
            {
                case HttpResponseStatus.Success:
                    var (data, exception) = Json.Deserialize<T>(response.RawData);
                    return data is null
                        ? HttpResponse<T>.ParseError(exception, response.RawData, response.StatusCode)
                        : HttpResponse<T>.Success(data);

                case HttpResponseStatus.TimedOut:
                    return HttpResponse<T>.TimedOut();

                default:
                    return HttpResponse<T>.Error(response.Exception, response.StatusCode);
            }
        }

        private HttpMethod ToHttpMethod(Method method) => method switch
        {
            Method.Get => HttpMethod.Get,
            Method.Post => HttpMethod.Post,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
