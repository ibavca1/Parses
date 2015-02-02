using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.LogoRu
{
    internal static class LogoRuConstants
    {
        public const int Id = -1;

        public const string Name = "Logo.Ru";

        public const string SiteUri = "http://www.logo.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly LogoRuCity[] SupportedCities = LogoRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("Автомобильная техника","Автосигнализация"),
new WebSiteMapPath("Встраиваемая техника","Запчасти для встраиваемой техники"),
new WebSiteMapPath("Компьютерная техника","Антивирусные программы"),
new WebSiteMapPath("Компьютерная техника","Игры для PC"),
new WebSiteMapPath("Компьютерная техника","Мебель компьютерная"),
new WebSiteMapPath("Красота и здоровье","Запчасти и аксессуары"),
new WebSiteMapPath("Телевизоры, аудио-видео","Светильники"),
new WebSiteMapPath("Техника для дома","Аксессуары и запчасти"),
new WebSiteMapPath("Техника для дома","Бытовая химия"),
new WebSiteMapPath("Техника для дома","Гладильные доски"),
new WebSiteMapPath("Техника для дома","Лампы электрические"),
new WebSiteMapPath("Техника для дома","Пылесборники и фильтры для пылесосов"),
new WebSiteMapPath("Техника для дома","Сушилки для белья"),
new WebSiteMapPath("Техника для дома","Часы"),
new WebSiteMapPath("Техника для кухни","Бытовая химия"),
new WebSiteMapPath("Техника для кухни","Запчасти для кухонной техники"),
new WebSiteMapPath("Фото- и видеокамеры","Фильтр"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "Инструменты"
                };
    }
}