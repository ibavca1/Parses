using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TdPoiskRu
{
    internal static class TdPoiskRuConstants
    {
        public const int Id = 84;

        public const string Name = "TdPoisk.Ru";

        public const string SiteUri = "http://tdPoisk.ru/";

        public const string UriMask = "http://{0}.tdpoisk.ru/";

        public const bool Articles = true;

        public static readonly WebCity[] Cities = TdPoiskRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ShopsAvailability = true;

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Для дома","Аксессуары"),
new WebSiteMapPath("Для дома","Уборка","Пылесборники"),
new WebSiteMapPath("Для дома","Уход за одеждой","Гладильные доски"),
new WebSiteMapPath("Для дома","Уход за одеждой","Стиральные порошки"),
new WebSiteMapPath("Для дома","Электротовары","Светильники"),
new WebSiteMapPath("Для дома","Электротовары","Стабилизаторы напряжения"),
new WebSiteMapPath("Для дома","Электротовары","Электролампы"),
new WebSiteMapPath("Для кухни","Кухонные принадлежности","Кухонная утварь"),
new WebSiteMapPath("Для кухни","Кухонные принадлежности","Посуда"),
new WebSiteMapPath("Для кухни","Кухонные принадлежности","Хлебницы"),
new WebSiteMapPath("Компьютерная техника","Аксессуары","Коврики"),
new WebSiteMapPath("Компьютерная техника","Аксессуары","Сетевые и Интерфейсные Шнуры"),
new WebSiteMapPath("Компьютерная техника","Другое","Антивирусы"),
new WebSiteMapPath("Компьютерная техника","Другое","Компьютерная мебель"),
new WebSiteMapPath("Компьютерная техника","Другое","Программное обеспечение"),
new WebSiteMapPath("Развлечения и игры","Игрушки"),
new WebSiteMapPath("Развлечения и игры","Игрушки на управлении"),
new WebSiteMapPath("Развлечения и игры","Игры для игровых приставок"),
new WebSiteMapPath("ТВ/Видео","Аксессуары","Чехлы для ПДУ"),
new WebSiteMapPath("Телефоны","Дата кабели"),
new WebSiteMapPath("Телефоны","Защитные пленки"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}