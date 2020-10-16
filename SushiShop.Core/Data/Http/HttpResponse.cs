using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace SushiShop.Core.Data.Http
{
    public enum HttpResponseStatus
    {
        Success,
        Error,
        TimedOut
    }

    public readonly struct HttpResponse
    {
        private HttpResponse(HttpResponseStatus responseStatus, string? data, Exception? exception, HttpStatusCode? statusCode)
        {
            ResponseStatus = responseStatus;
            Data = data;
            Exception = exception;
            StatusCode = statusCode;
        }

        public readonly HttpResponseStatus ResponseStatus { get; }

        public readonly string? Data { get; }

        public readonly Exception? Exception { get; }

        public readonly HttpStatusCode? StatusCode { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpResponse Success(string? data, HttpStatusCode statusCode)
            => new HttpResponse(HttpResponseStatus.Success, data, exception: null, statusCode);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpResponse Error(Exception? exception, HttpStatusCode? statusCode)
            => new HttpResponse(HttpResponseStatus.Error, data: null, exception, statusCode);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpResponse TimedOut() =>
            new HttpResponse(HttpResponseStatus.TimedOut, data: null, exception: null, statusCode: null);

        public readonly override string ToString() =>
            $"Response status: {ResponseStatus}, status code: {StatusCode}, exception message: {Exception?.Message}";
    }
}
