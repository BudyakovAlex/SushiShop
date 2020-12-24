using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Providers;

namespace SushiShop.Core.Services.Http
{
    public class HttpService : IHttpService
    {
        private const string BaseUrl = "https://sushishop.ru/api/";

        private readonly IUserSession userSession;
        private readonly HttpClient client;

        public HttpService(IUserSession userSession)
        {
            this.userSession = userSession;

            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.Timeout = TimeSpan.FromMinutes(5);
        }

        public Task<HttpResponse<T>> ExecuteAnonymouslyAsync<T>(Method method, string url, object? content, CancellationToken cancellationToken)
            where T : class
        {
            var request = CreateRequestMessage(method, url, content);
            return ExecuteAsync<T>(request, cancellationToken);
        }

        public Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, object? content, CancellationToken cancellationToken)
            where T : class
        {
            var request = CreateRequestMessage(method, url, content);
            AddAuthorizationHeaderIfExists(request);

            return ExecuteAsync<T>(request, cancellationToken);
        }

        public Task<HttpResponse<T>> ExecuteMultipartAsync<T>(Method method, string url, object? content, string[] filePaths, CancellationToken cancellationToken)
            where T : class
        {
            var multipartContent = new MultipartContent();

            var stringContent = CreateStringContentOrDefault(content);
            if (stringContent != null)
            {
                multipartContent.Add(stringContent);
            }

            foreach (var filePath in filePaths)
            {
                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                multipartContent.Add(new StreamContent(fs));
            }

            var request = CreateRequestMessage(method, url);
            request.Content = multipartContent;

            AddAuthorizationHeaderIfExists(request);

            return ExecuteAsync<T>(request, cancellationToken);
        }

        private async Task<HttpResponse<T>> ExecuteAsync<T>(HttpRequestMessage request, CancellationToken cancellationToken)
            where T : class
        {
            var response = await ExecuteAsync(request, cancellationToken);
            return Deserialize<T>(response);
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
                    var (data, exception) = Json.SafeDeserialize<T>(response.RawData);
                    return data is null
                        ? HttpResponse<T>.ParseError(exception, response.RawData, response.StatusCode)
                        : HttpResponse<T>.Success(data);

                case HttpResponseStatus.TimedOut:
                    return HttpResponse<T>.TimedOut();

                default:
                    return HttpResponse<T>.Error(response.Exception, response.StatusCode);
            }
        }

        private void AddAuthorizationHeaderIfExists(HttpRequestMessage requestMessage)
        {
            var token = userSession.GetToken();
            if (token != null)
            {
                requestMessage.Headers.Add(token.Header, $"{token.HeaderPreffix}{token.AccessToken}");
            }
        }

        private HttpRequestMessage CreateRequestMessage(Method method, string url, object? content = null)
        {
            var httpMethod = ToHttpMethod(method);
            var request = new HttpRequestMessage(httpMethod, url);
            request.Content = CreateStringContentOrDefault(content);

            return request;

            static HttpMethod ToHttpMethod(Method method) => method switch
            {
                Method.Get => HttpMethod.Get,
                Method.Post => HttpMethod.Post,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        private StringContent? CreateStringContentOrDefault(object? content)
        {
            switch (content)
            {
                case null:
                    return null;

                case string str:
                    return new StringContent(str);

                case object obj:
                    var value = Json.Serialize(obj);
                    return new StringContent(value);
            }
        }
    }
}