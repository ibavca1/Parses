using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EldoradoRu
{
    internal static class EldoradoRuConstants
    {
        public const int Id = 37;

        public const string Name = "Eldorado.Ru";

        public const string SiteUri = "http://www.eldorado.ru/";

        public const string SiteUriPrefix = "http://{0}.eldorado.ru/";

        public const bool AvailabilityInShops = true;

        public const bool ProductArticle = true;

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(7);

        public static readonly WebCity[] Cities = EldoradoRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("Телевизоры и видео","DVD, Blu-Ray, Видео",	"Фильмы"),
new WebSiteMapPath("Автомобильная электроника","Аксессуары для автотехники","Кабели/Переходники"),
new WebSiteMapPath("Автомобильная электроника","Аксессуары для автотехники","Конденсаторы"),
new WebSiteMapPath("Игры, софт, развлечения","Игрушки"),
new WebSiteMapPath("Игры, софт, развлечения","Игры"),
new WebSiteMapPath("Игры, софт, развлечения","Кино"),
new WebSiteMapPath("Игры, софт, развлечения","Книги"),
new WebSiteMapPath("Игры, софт, развлечения","Софт"),
new WebSiteMapPath("Компьютеры, ноутбуки и планшеты","Компьютерные аксессуары","Защитные пленки"),
new WebSiteMapPath("Компьютеры, ноутбуки и планшеты","Мебель"),
new WebSiteMapPath("Компьютеры, ноутбуки и планшеты","Устройства ввода","Коврики для мыши"),
new WebSiteMapPath("Красота и здоровье","Личная гигиена","Аксессуары для бритв"),
new WebSiteMapPath("Красота и здоровье","Личная гигиена","Зубные щетки","Насадки"),
new WebSiteMapPath("Телефоны и связь","Аксессуары для телефонов","Защитные пленки"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для пароочистителей"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для пылесосов"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для пылесосов","Насадки и щетки"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для пылесосов","Пылесборники"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для пылесосов","Фильтры"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для стиральных машин"),
new WebSiteMapPath("Техника для дома","Аксессуары","Аксессуары для увлажнителей и очистителей воздуха"),
new WebSiteMapPath("Техника для дома","Аксессуары","Бытовая химия"),
new WebSiteMapPath("Техника для дома","Аксессуары","Бытовая химия","Чистящие средства"),
new WebSiteMapPath("Техника для дома","Аксессуары","Лампочки"),
new WebSiteMapPath("Техника для дома","Аксессуары","Прочее"),
new WebSiteMapPath("Техника для дома","Аксессуары","Светильники"),
new WebSiteMapPath("Техника для дома","Аксессуары","Часы и будильники"),
new WebSiteMapPath("Техника для дома","Утюги и гладильные принадлежности","Аксессуары для утюгов"),
new WebSiteMapPath("Техника для дома","Утюги и гладильные принадлежности","Гладильные доски"),
new WebSiteMapPath("Техника для дома","Утюги и гладильные принадлежности","Сушки для белья"),
new WebSiteMapPath("Техника для кухни","Аксессуары"),
new WebSiteMapPath("Техника для кухни","Посуда","Контейнеры"),
new WebSiteMapPath("Техника для кухни","Посуда","Ножи"),
new WebSiteMapPath("Техника для кухни","Посуда","Стеклянная посуда"),
new WebSiteMapPath("Техника для кухни","Посуда","Столовые приборы"),
new WebSiteMapPath("Техника для кухни","Посуда","Терки"),
new WebSiteMapPath("Товары для дома, сада и ремонта","Аксессуары для дома, сада и ремонта"),
new WebSiteMapPath("Товары для дома, сада и ремонта","Инструменты"),
new WebSiteMapPath("Товары для дома, сада и ремонта","Кемпинг"),
new WebSiteMapPath("Товары для дома, сада и ремонта","Полив и орошение"),
new WebSiteMapPath("Товары для дома, сада и ремонта","Садовая техника"),
new WebSiteMapPath("Товары для дома, сада и ремонта","Товары для отдыха"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Товары для детей и мам"
                };
    }
}