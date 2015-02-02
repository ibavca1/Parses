using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.CorpCentreRu
{
    internal static class CorpCentreRuConstants
    {
        public const int Id = 79;

        public const string Name = "Centre.Ru";

        public const string SiteUri = "http://www.corpcentre.ru/";

        public const bool Articles = true;

        public static readonly WebCity[] Cities = CorpCentreRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(6);

        public const bool ShopsAvailability = true;

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("Автомобильная электроника"),
new WebSiteMapPath("Аудио","Hi-Fi техника","Фонокорректоры"),
new WebSiteMapPath("Аудио","Музыкальные инструменты","Аксессуары для синтезаторов"),
new WebSiteMapPath("Компьютеры и оргтехника","Игровые консоли","Игры для игровых приставок"),
new WebSiteMapPath("Компьютеры и оргтехника","Манипуляторы и клавиатуры","Коврики для компьютерных мышек"),
new WebSiteMapPath("Телевизоры, видео","Периферия для Smart TV"),
new WebSiteMapPath("Телефоны и связь","Держатели для телефонов"),
new WebSiteMapPath("Телефоны и связь","Защитные пленки"),
new WebSiteMapPath("Техника для дома","Аксессуары для дома"),
new WebSiteMapPath("Техника для дома","Электроинструмент"),
new WebSiteMapPath("Техника для кухни","Встраиваемая техника","Аксессуары для встраиваемой техники"),
new WebSiteMapPath("Техника для кухни","Готовим напитки","Аксессуары для кофеварок"),
new WebSiteMapPath("Техника для кухни","Крупная кухонная техника","Аксессуары к холодильникам"),
new WebSiteMapPath("Техника для кухни","Кухонная утварь","Наборы столовых приборов"),
new WebSiteMapPath("Техника для кухни","Кухонная утварь","Ножи"),
new WebSiteMapPath("Техника для кухни","Кухонная утварь","Прочая"),
new WebSiteMapPath("Техника для кухни","Кухонная утварь","Шумовки, половники, лопатки, венчики"),
new WebSiteMapPath("Техника для кухни","Малая бытовая техника","Полезности для мультиварок"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "Спорт и отдых"
                };
    }
}