using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnosilaRu
{
    internal static class TehnosilaRuConstants
    {
        public const int Id = 69;

        public const string Name = "Tehnosila.Ru";

        public const string SiteUri = "http://www.tehnosila.ru/";

        public const string UriMask = "http://{0}.tehnosila.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(3);

        public const bool SupportProductsArticle = true;

        public const bool SupportAvailabilityInShops = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly TehnosilaRuCity[] SupportedCities = TehnosilaRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Встраиваемая техника", "Аксессуары для встраиваемой техники"),
                    new WebSiteMapPath("Для дома, дачи и ремонта", "Все для пикника"),
                    new WebSiteMapPath("Для дома, дачи и ремонта", "Ручной инструмент"),
                    new WebSiteMapPath("Для дома, дачи и ремонта", "Садовая мебель и элементы интерьера"),
                    new WebSiteMapPath("Для дома, дачи и ремонта", "Техника для дачи"),
                    new WebSiteMapPath("Для дома, дачи и ремонта", "Электроинструмент"),
                    new WebSiteMapPath("Игры, софт, развлечения", "Игры для игровых приставок"),
                    new WebSiteMapPath("Игры, софт, развлечения", "Программное обеспечение"),
                    new WebSiteMapPath("Компьютеры, ноутбуки, планшеты", "Мебель для компьютера"),
                    new WebSiteMapPath("Техника для дома", "Безопасность дома"),
                    new WebSiteMapPath("Техника для кухни", "Аксессуары для кухонной техники"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Товары для спорта и отдыха",
                    "Солнцезащитные очки",
                    "Товары для детей",
                    "Подарочные карты",
                    "Подарки и сувениры",
                    "Радиоуправляемые модели",
                    "Игрушки",
                    "Фильмы"
                };
    }
}