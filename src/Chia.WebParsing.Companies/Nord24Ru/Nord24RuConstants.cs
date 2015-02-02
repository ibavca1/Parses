using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.Nord24Ru
{
    internal static class Nord24RuConstants
    {
        public const int Id = 74;

        public const string Name = "Nord24.Ru";

        public const string SiteUri = "http://www.nord24.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly Nord24RuCity[] SupportedCities = Nord24RuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("Автомобильная техника","Автосигнализация"),
new WebSiteMapPath("Автомобильная техника","Аксессуары"),
new WebSiteMapPath("Встраиваемая техника","Запчасти для встраиваемой техники"),
new WebSiteMapPath("Компьютерная техника","Антивирусные программы"),
new WebSiteMapPath("Компьютерная техника","Мебель компьютерная"),
new WebSiteMapPath("Телевизоры, аудио-видео","Светильники"),
new WebSiteMapPath("Техника для дома","Аксессуары и запчасти"),
new WebSiteMapPath("Техника для дома","Бытовая химия"),
new WebSiteMapPath("Техника для дома","Гладильные доски"),
new WebSiteMapPath("Техника для дома","Лампы электрические"),
new WebSiteMapPath("Техника для дома","Пылесборники и фильтры для пылесосов"),
new WebSiteMapPath("Техника для кухни","Запчасти для кухонной техники"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "Инструменты"
                };
    }
}