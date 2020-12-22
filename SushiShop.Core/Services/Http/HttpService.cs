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

        public Task<HttpResponse<T>> ExecuteAnonymouslyAsync<T>(Method method, string url, object? content, CancellationToken cancellationToken) where T : class
        {
            var request = CreateRequestMessage(method, url, content);
            return ExecuteAsync<T>(request, cancellationToken);
        }

        public Task<HttpResponse<T>> ExecuteAsync<T>(Method method, string url, object? content, CancellationToken cancellationToken) where T : class
        {
            var request = CreateRequestMessage(method, url, content);
            AddTokenIfPossible(request);

            return ExecuteAsync<T>(request, cancellationToken);
        }

        public Task<HttpResponse<T>> ExecuteMultipartAsync<T>(Method method, string url, object body, string[]? files, CancellationToken cancellationToken) where T : class
        {
            var content = Json.Serialize(body);
            var multipartContent = new MultipartContent
            {
                new StringContent(content)
            };

            if (files != null)
            {
                foreach (var file in files)
                {
                    using var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                    multipartContent.Add(new StreamContent(fs));
                }
            }

            var request = new HttpRequestMessage(ToHttpMethod(method), url)
            {
                Content = new StringContent(content)
            };

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

        private void AddTokenIfPossible(HttpRequestMessage requestMessage)
        {
            var token = userSession.GetToken();
            if (token != null)
            {
                requestMessage.Headers.Add(token.Header, $"{token.HeaderPreffix}{token.AccessToken}");
            }
        }

        private HttpRequestMessage CreateRequestMessage(Method method, string url, object? content)
        {
            var httpMethod = ToHttpMethod(method);
            var request = new HttpRequestMessage(httpMethod, url);

            switch (content)
            {
                case string str:
                    request.Content = new StringContent(str);
                    break;

                case object obj:
                    var value = Json.Serialize(obj);
                    request.Content = new StringContent(value);
                    break;
            }

            return request;
        }

        private HttpMethod ToHttpMethod(Method method) => method switch
        {
            Method.Get => HttpMethod.Get,
            Method.Post => HttpMethod.Post,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}