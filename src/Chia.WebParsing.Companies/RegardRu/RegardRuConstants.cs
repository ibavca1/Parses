using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RegardRu
{
    internal static class RegardRuConstants
    {
        public const int Id = 420;

        public const string Name = "Regard.Ru";

        public const string SiteUri = "http://www.regard.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(2);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly RegardRuCity[] SupportedCities = RegardRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Аксессуары для мобильных устройств", "Защитные плёнки"),
                    new WebSiteMapPath("Аксессуары для мобильных устройств", "Стилусы"),
                    new WebSiteMapPath("Оборудование для презентаций", "Аксессуары"),
                    new WebSiteMapPath("Оборудование для презентаций", "Лампы для проекторов"),
                    new WebSiteMapPath("Офисная техника", "Брошюровщики (переплетчики)"),
                    new WebSiteMapPath("Офисная техника", "Принтеры этикеток"),
                    new WebSiteMapPath("Офисная техника", "Резаки"),
                    new WebSiteMapPath("Офисная техника", "Уничтожители бумаг (шредеры)"),
                    new WebSiteMapPath("Память Flash", "Кардридеры"),
                    new WebSiteMapPath("Плоттеры", "CANON"),
                    new WebSiteMapPath("Плоттеры", "EPSON"),
                    new WebSiteMapPath("Плоттеры", "HP"),
                    new WebSiteMapPath("Плоттеры", "Аксессуары к плоттерам")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                         "Программное обеспечение",
                         "Подарки",
                         "Уцененный товар"
                };
    }
}