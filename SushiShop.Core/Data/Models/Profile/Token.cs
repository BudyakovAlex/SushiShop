using System;

namespace SushiShop.Core.Data.Models.Profile
{
    public class Token
    {
        public Token(string accessToken, string header, string headerPreffix, DateTimeOffset expiresAt)
        {
            AccessToken = accessToken;
            Header = header;
            HeaderPreffix = headerPreffix;
            ExpiresAt = expiresAt;
        }

        public string AccessToken { get; }

        public string Header { get; }

        public string HeaderPreffix { get; }

        public DateTimeOffset ExpiresAt { get; }
    }
}
