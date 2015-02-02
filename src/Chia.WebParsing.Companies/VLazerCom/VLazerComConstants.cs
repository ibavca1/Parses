using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.VLazerCom
{
    internal static class VLazerComConstants
    {
        public const int Id = 196;

        public const string Name = "V-Lazer.Ru";

        public const string SiteUri = "http://shop.v-lazer.com";

        public const string SiteUriMask = "http://{0}.shop.v-lazer.com";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly WebCity[] SupportedCities = VLazerComCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Компьютерная техника","Cумки","Сумки для дисков"),
new WebSiteMapPath("Компьютерная техника","Программное обеспечение"),
new WebSiteMapPath("Компьютерная техника","Устройства чтения и хранения","USB Концентраторы"),
new WebSiteMapPath("Посуда","Кухонные принадлежности"),
new WebSiteMapPath("Посуда","Наборы кухонных аксессуаров"),
new WebSiteMapPath("Посуда","Наборы ножей"),
new WebSiteMapPath("Посуда","Наборы посуды"),
new WebSiteMapPath("Посуда","Столовые приборы"),
new WebSiteMapPath("Посуда","Товары для отдыха"),
new WebSiteMapPath("Посуда","Товары для отдыха","Треноги"),
new WebSiteMapPath("Посуда","Формы для выпечки, противени"),
new WebSiteMapPath("Телевизоры, аудио, видео","Аксессуары","Средства для ухода"),
new WebSiteMapPath("Техника для дома","Лампы и светильники","Настольные лампы"),
new WebSiteMapPath("Техника для дома","Лампы и светильники","Светильники"),
new WebSiteMapPath("Техника для дома","Пылесосы","Аксессуары"),
new WebSiteMapPath("Техника для дома","Техника для глажки","Гладильные доски"),
new WebSiteMapPath("Техника для кухни","Приготовление кофе","Капсулы"),
new WebSiteMapPath("Техника для кухни","Средства для ухода"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}