using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EnterRu
{
    internal static class EnterRuConstants
    {
        public const int Id = 556;

        public const string Name = "Enter.Ru";

        public const string SiteUri = "http://www.enter.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly EnterRuCity[] SupportedCities = EnterRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
                    
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Tchibo",
                    "Сделай сам",
                    "Товары для дома",
                    "Мебель",
                    "Детские товары",
                    "Украшения и часы",
                    "Парфюми косметика",
                    "Подаркии хобби",
                    "Зоотовары",
                    "Спорти отдых"
                };
    }
}