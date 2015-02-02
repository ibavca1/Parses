using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.CentrBtRu
{
    internal static class CentrBtRuConstants
    {
        public const int Id = 620;

        public const string Name = "CentrBt.Ru";

        public const string SiteUri = "http://www.centrbt.ru";

        public const bool Articles = false;

        public static readonly WebCity[] Cities = CentrBtRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ShopsAvailability = false;

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Аксессуары"),
                    new WebSiteMapPath("Аксессуары", "Акессуары для СВЧ"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для вытяжек"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для духовок"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для климатической техники"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для кофемашин"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для кухонной техники"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для плит"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для посудомоечных машин"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для пылесосов"),
                    new WebSiteMapPath("Аксессуары", "Аксессуары для стиральных машин"),
                    new WebSiteMapPath("Встраиваемая бытовая техника", "Измельчители бытовых отходов"),
                    new WebSiteMapPath("Встраиваемая бытовая техника", "Кухонные мойки"),
                    new WebSiteMapPath("Встраиваемая бытовая техника", "Кухонные мойки", "Мойки"),
                    new WebSiteMapPath("Для автомобиля", "Автомобильные шины"),
                    new WebSiteMapPath("Для автомобиля", "Автомобильные шины", "Всесезонные шины"),
                    new WebSiteMapPath("Для автомобиля", "Автомобильные шины", "Зимние шины"),
                    new WebSiteMapPath("Для автомобиля", "Автосигнализации"),
                    new WebSiteMapPath("Для автомобиля", "Аксессуары"),
                    new WebSiteMapPath("Для автомобиля", "Аксессуары", "Автомобильные компрессоры"),
                    new WebSiteMapPath("Для автомобиля", "Детские автомобильные кресла"),
                    new WebSiteMapPath("Компьютеры, оргтехника", "Аксессуары", "Чистящие средства"),
                    new WebSiteMapPath("Компьютеры, оргтехника", "Комплектующие и софт"),
                    new WebSiteMapPath("Мелкая бытовая техника", "Гладильные доски"),
                    new WebSiteMapPath("Мелкая бытовая техника", "Кухонная посуда и принадлежности", "Для чая и кофе"),
                    new WebSiteMapPath("Мелкая бытовая техника", "Кухонная посуда и принадлежности",
                                       "Кухонные принадлежности"),
                    new WebSiteMapPath("Мелкая бытовая техника", "Кухонная посуда и принадлежности", "Ножи"),
                    new WebSiteMapPath("Мелкая бытовая техника", "Средства по уходу за техникой"),
                    new WebSiteMapPath("Специальные предложения", "Стульчики для кормления"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Спортивные товары",
                    "Для дома и дачи",
                    "Товары для детей",
                    "Музыкальные инструменты"
                };
    }
}