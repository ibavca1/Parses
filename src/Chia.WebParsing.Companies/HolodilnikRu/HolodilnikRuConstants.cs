using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.HolodilnikRu
{
    internal static class HolodilnikRuConstants
    {
        public const int Id = 610;

        public const string Name = "Holodolnik.Ru";

        public const string SiteUri = "http://www.holodilnik.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly HolodilnikRuCity[] SupportedCities = HolodilnikRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Бытовая техника для кухни", "Подготовка и обработка продуктов", "Аксессуары"),
new WebSiteMapPath("Бытовая техника для дома","Аксессуары"),
new WebSiteMapPath("Бытовая техника для дома","Техника для приусадебного участка"),
new WebSiteMapPath("Бытовая техника для дома","Техника для ухода за одеждой","Гладильные доски"),
new WebSiteMapPath("Бытовая техника для дома","Техника для ухода за одеждой","Сушилки для белья"),
new WebSiteMapPath("Бытовая техника для дома","Электроинструменты"),
new WebSiteMapPath("Бытовая техника для кухни","Аксессуары и сопутствующие товары"),
new WebSiteMapPath("Бытовая техника для кухни","Газирование воды"),
new WebSiteMapPath("Бытовая техника для кухни","Измельчители пищевых отходов"),
new WebSiteMapPath("Бытовая техника для кухни","Кофейное оборудование","Аксессуары"),
new WebSiteMapPath("Бытовая техника для кухни","Кофейное оборудование","Кофе и чай"),
new WebSiteMapPath("Бытовая техника для кухни","Кухонная посуда и принадлежности","Ёмкости для хранения продуктов"),
new WebSiteMapPath("Бытовая техника для кухни","Кухонная посуда и принадлежности","Наборы посуды для СВЧ-печей"),
new WebSiteMapPath("Бытовая техника для кухни","Кухонная посуда и принадлежности","Ножи кухонные"),
new WebSiteMapPath("Бытовая техника для кухни","Обработка и очистка воды","Кулеры для воды"),
new WebSiteMapPath("Посудомоечные и стиральные  машины","Аксессуары"),
new WebSiteMapPath("Телевизоры, DVD,   аудио-видео техника","Сопутствующее оборудование и аксессуары","Чистящие средства"),
new WebSiteMapPath("Холодильники и морозильники","Аксессуары и сопутствующие товары для холодильников"),
new WebSiteMapPath("Холодильники и морозильники","Льдогенераторы"),
new WebSiteMapPath("Холодильники и морозильники","Сигарные шкафы (хьюмидоры)"),
new WebSiteMapPath("Холодильники и морозильники","Системы охлаждения и розлива пива"),
new WebSiteMapPath("Цифровая техника, ноутбуки","Сопутствующее оборудование и аксессуары","Защитные пленки и стекла"),
new WebSiteMapPath("Цифровая техника, ноутбуки","Сопутствующее оборудование и аксессуары","Коврики для мышек"),
new WebSiteMapPath("Цифровая техника, ноутбуки","Сопутствующее оборудование и аксессуары","Программное обеспечение"),
new WebSiteMapPath("Цифровая техника, ноутбуки","Сопутствующее оборудование и аксессуары","Средства для ухода за техникой")
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}