using System;
using System.Text;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.IonRu
{
    internal static class IonRuConstants
    {
        public const int Id = 424;

        public const string Name = "Ion.Ru";

        public const string SiteUri = "http://i-on.ru/";

        public const bool Articles = true;

        public static readonly Encoding Encoding = Encoding.UTF8;

        public static readonly WebCity[] Cities = new WebCity[]{WebCities.Moscow};

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ShopsAvailability = false;

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Обзоры",
                    "Видео",
                    "Инструкции",
                    "Все товары"
                };
    }
}