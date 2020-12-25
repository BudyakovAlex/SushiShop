using System;
using System.Net;

namespace SushiShop.Core.Data.Http
{
    public class HttpResponse<T> : HttpResponse
        where T : class
    {
        protected HttpResponse(
            HttpResponseStatus responseStatus,
            string? rawData,
            Exception? exception,
            HttpStatusCode? statusCode,
            T? data)
            : base(responseStatus, rawData, exception, statusCode)
        {
            Data = data;
        }

        public T? Data { get; }

        public static HttpResponse<T> Success(T data) =>
            new HttpResponse<T>(HttpResponseStatus.Success, rawData: null, exception: null, statusCode: null, data);

        public static HttpResponse<T> ParseError(Exception? exception, string? rawData, HttpStatusCode? statusCode) =>
            new HttpResponse<T>(HttpResponseStatus.ParseError, rawData, exception, statusCode, data: null);

        public static new HttpResponse<T> Error(Exception? exception, HttpStatusCode? statusCode) =>
            new HttpResponse<T>(HttpResponseStatus.Error, rawData: null, exception, statusCode, null);

        public static new HttpResponse<T> TimedOut() =>
            new HttpResponse<T>(HttpResponseStatus.TimedOut, rawData: null, exception: null, statusCode: null, data: null);
    }

    public class HttpResponse
    {
        protected HttpResponse(HttpResponseStatus responseStatus, string? rawData, Exception? exception, HttpStatusCode? statusCode)
        {
            ResponseStatus = responseStatus;
            RawData = rawData;
            Exception = exception;
            StatusCode = statusCode;
        }

        public HttpResponseStatus ResponseStatus { get; }

        public string? RawData { get; }

        public Exception? Exception { get; }

        public HttpStatusCode? StatusCode { get; }

        public bool IsSuccessful => ResponseStatus == HttpResponseStatus.Success;

        public static HttpResponse Success(string rawData, HttpStatusCode statusCode)
            => new HttpResponse(HttpResponseStatus.Success, rawData, exception: null, statusCode);

        public static HttpResponse Error(Exception? exception, HttpStatusCode? statusCode)
            => new HttpResponse(HttpResponseStatus.Error, rawData: null, exception, statusCode);

        public static HttpResponse Error(string rawData, HttpStatusCode? statusCode)
           => new HttpResponse(HttpResponseStatus.Error, rawData, exception: null, statusCode);

        public static HttpResponse TimedOut() =>
            new HttpResponse(HttpResponseStatus.TimedOut, rawData: null, exception: null, statusCode: null);

        public override string ToString() =>
            $"Response status: {ResponseStatus}, status code: {StatusCode}, exception message: {Exception?.Message}";
    }
}