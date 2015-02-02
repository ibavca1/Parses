using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.NotikRu
{
    internal static class NotikRuConstants
    {
        public const int Id = 608;

        public const string Name = "Notik.Ru";

        public const string SiteUri = "http://www.notik.ru/";

        public const string MobileSiteUri = "http://m.notik.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public static readonly WebCity[] Cities = NotikRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Аксессуары", "Средства ухода"),
                    new WebSiteMapPath("Портативные устройства", "Часы и браслеты"), 
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Игры и программы"
                };
    }
}