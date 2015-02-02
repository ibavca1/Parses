using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TelemaksRu
{
    internal static class TelemaksRuConstants
    {
        public const int Id = 117;

        public const string Name = "Telemaks.Ru";

        public const string SiteUri = "http://www.telemaks.ru/";

        public const string SiteUriMask = "http://{0}.telemaks.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly WebCity[] SupportedCities = TelemaksRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
                    
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}