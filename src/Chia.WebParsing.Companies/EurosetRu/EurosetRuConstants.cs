using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.EurosetRu
{
    internal static class EurosetRuConstants
    {
        public const int Id = 40;

        public const string Name = "Euroset.Ru";

        public const string SiteUri = "http://euroset.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly EurosetRuCity[] SupportedCities = EurosetRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("БЫТОВАЯТЕХНИКА","Аксессуары","Для приготовления кофе"),
new WebSiteMapPath("БЫТОВАЯТЕХНИКА","Аксессуары","Для приготовления чая"),
new WebSiteMapPath("БЫТОВАЯТЕХНИКА","Аксессуары","Для пылесосов"),
new WebSiteMapPath("БЫТОВАЯТЕХНИКА","Аксессуары","Чехлы для гладильных досок"),
new WebSiteMapPath("БЫТОВАЯТЕХНИКА","Аксессуары для бытовой техники"),
new WebSiteMapPath("БЫТОВАЯТЕХНИКА","Банковская и кассовая техника"),
new WebSiteMapPath("КНИГИИ ПОДАРКИ","Книги"),
new WebSiteMapPath("КНИГИИ ПОДАРКИ","Кожгалантерея"),
new WebSiteMapPath("КНИГИИ ПОДАРКИ","Кожгалантерея","Обложки и визитницы"),
new WebSiteMapPath("КНИГИИ ПОДАРКИ","Подарочные сертификаты"),
new WebSiteMapPath("КНИГИИ ПОДАРКИ", "Подарочные сертификаты и карты оплаты"),
new WebSiteMapPath("ПЛАНШЕТЫНОУТБУКИ","Аксессуары","Коврики"),
new WebSiteMapPath("ПЛАНШЕТЫНОУТБУКИ","Аксессуары","Подставки для ноутбуков"),
new WebSiteMapPath("ПЛАНШЕТЫНОУТБУКИ","Аксессуары","Средства для ухода"),
new WebSiteMapPath("ПЛАНШЕТЫНОУТБУКИ","Программы"),
new WebSiteMapPath("Телефоны","Luxury","Цепочки"),
new WebSiteMapPath("Телефоны","Тарифные планы и Sim-карты"),
new WebSiteMapPath("Телефоны","Аксессуары","Защитная плёнка"),
new WebSiteMapPath("Телефоны","Аксессуары","Наклейки"),
new WebSiteMapPath("Телефоны","Аксессуары","Перчатки"),
new WebSiteMapPath("Телефоны","Аксессуары","Подарочная упаковка"),
new WebSiteMapPath("Телефоны","Аксессуары","Стилусы"),
new WebSiteMapPath("Телефоны","Программы"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Кухонная утварь","Аксессуары для кухни"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Кухонная утварь","Доски разделочные"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Кухонная утварь","Точилки для ножей"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Кухонная утварь","Хлебницы, масленки, сахарницы"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Освещение","Светильники"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Освещение","Энергосберегающие лампы"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Посуда","Контейнеры"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Посуда","Столовые приборы"),
new WebSiteMapPath("ТОВАРЫДЛЯ ДОМА","Электроинструменты"),
new WebSiteMapPath("Электроника","Для Авто","Аксессуары","Защитные пленки"),
new WebSiteMapPath("Электроника","Игры и развлечения","Аксессуары","Защитные пленки"),
new WebSiteMapPath("Электроника","Игры и развлечения","Желтый товар"),
new WebSiteMapPath("Электроника","Игры и развлечения","Товары для спорта"),
new WebSiteMapPath("Электроника","Игры и развлечения","Фильмы"),
new WebSiteMapPath("Электроника","Игры и развлечения","Электронные игры"),
new WebSiteMapPath("Электроника","Портативная техника","Часы"),
new WebSiteMapPath("Электроника","Фото и видео","Аксессуары","Карандаши для оптики/экранов"),
new WebSiteMapPath("Электроника","Фото и видео","Аксессуары","Фильтры"),

                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "ТОВАРЫДЛЯ ДЕТЕЙ"
                };
    }
}