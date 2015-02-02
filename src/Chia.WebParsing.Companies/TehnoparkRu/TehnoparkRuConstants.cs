using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnoparkRu
{
    internal static class TehnoparkRuConstants
    {
        public const int Id = 62;

        public const string Name = "Tehnopark.Ru";

        public const string SiteUri = "http://www.technopark.ru/";

        public const string UriMask = "http://{0}.technopark.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportAvailabilityInShops = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = TehnoparkRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
                   
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Электроинструменты", 
                    "Расходные материалы", 
                    "Чистящие средства", 
                    "Уцененные товары"
                };
    }
}