using BuildApps.Core.Mobile.Common.Extensions;
using HeyRed.Mime;
using SushiShop.Core.Common;
using SushiShop.Core.Data.Http;
using SushiShop.Core.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace SushiShop.Core.Services.Http
{
    public class HttpService : IHttpService
    {
        private const string BaseUrl = "https://sushishop.ru/api/";
        private const string FormDataDispositionType = "form-data";
        private const string FilesParameterName = "files[]";

        private readonly IUserSession userSession;
        private readonly HttpClient client;
        private readonly HttpStatusCode[] _notValidStatusesWithErrorContent = new[]
        {
            HttpStatusCode.Unauthorized,
            HttpStatusCode.BadRequest,
            HttpStatusCode.NotFound
        };

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

        public Task<HttpResponse<T>> ExecuteMultipartAsync<T>(Method method, string url, Dictionary<string, string>? parameters, string[] filePaths, CancellationToken cancellationToken)
            where T : class
        {
            parameters ??= new Dictionary<string, string>();

            var multipartContent = new MultipartFormDataContent();
            foreach (var parameter in parameters)
            {
                var stringContent = CreateStringContentOrDefault(parameter.Value);
                if (stringContent != null)
                {
                    multipartContent.Add(stringContent, parameter.Key);
                }
            }

            foreach (var filePath in filePaths)
            {
                if (!File.Exists(filePath))
                {
                    continue;
                }

                var fileInfo = new FileInfo(filePath);
                var fileName = $"{Guid.NewGuid()}{fileInfo.Extension}";

                var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                var content = new StreamContent(fs);

                var mimeType = MimeTypesMap.GetMimeType(fileInfo.Name);
                content.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue(FormDataDispositionType)
                {
                    FileName = fileName,
                    Name = FilesParameterName
                };

                multipartContent.Add(content);
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

                if (_notValidStatusesWithErrorContent.Contains(response.StatusCode))
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return HttpResponse.Error(data, response.StatusCode);
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
                        : HttpResponse<T>.Success(data, response.StatusCode);

                case HttpResponseStatus.TimedOut:
                    return HttpResponse<T>.TimedOut();

                case HttpResponseStatus.Error when response.RawData.IsNotNullNorEmpty():
                    var (errorData, parseException) = Json.SafeDeserialize<T>(response.RawData);
                    return errorData is null
                        ? HttpResponse<T>.ParseError(parseException, response.RawData, response.StatusCode)
                        : HttpResponse<T>.Error(errorData, response.StatusCode);

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
                Method.Put => HttpMethod.Put,
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