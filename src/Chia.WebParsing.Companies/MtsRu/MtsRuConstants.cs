using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.MtsRu
{
    internal static class MtsRuConstants
    {
        public const int Id = 128;

        public const string Name = "MTS.Ru";

        public const string SiteUri = "http://www.shop.mts.ru";

        public const string UriMask = "http://{0}.shop.mts.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = MtsRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Аксессуары для портативной техники", "Средства по уходу за техникой"),
new WebSiteMapPath("Аксессуары для телефонов", "Защитная пленка"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Тарифы",
                    "Фемтосоты",
                    "Домашний Интернет и TВ",
                    "Умный дом",
                    "Мобильные приложения",
                    "Модемы и роутеры 3G/4G"
                };
    }
}