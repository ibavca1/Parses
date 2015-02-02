using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.UlmartRu
{
    internal static class UlmartRuConstants
    {
        public const int Id = 534;

        public const string Name = "Ulmart.Ru";

        public const string SiteUri = "http://www.ulmart.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportAvailabilityInShops = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly UlmartRuCity[] SupportedCities = UlmartRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            {
new WebSiteMapPath("Автотовары","Запчасти для коммерческого транспорта"),
new WebSiteMapPath("Автотовары","Запчасти для ТО"),
new WebSiteMapPath("Автотовары","Крепления для лыж"),
new WebSiteMapPath("Автотовары","Автоаксессуары"),
new WebSiteMapPath("Автотовары","Автоинструмент"),
new WebSiteMapPath("Автотовары","Автолитература"),
new WebSiteMapPath("Автотовары","Аккумуляторные батареи"),
new WebSiteMapPath("Автотовары","Багажники, боксы на крышу, велокрепления"),
new WebSiteMapPath("Автотовары","Внешнее оснащение и тюнинг"),
new WebSiteMapPath("Автотовары","Запчасти для технического обслуживания"),
new WebSiteMapPath("Автотовары","Масла, автокосметика и автохимия"),
new WebSiteMapPath("Автотовары","МОТОтовары"),
new WebSiteMapPath("Автотовары","Шины, диски"),
new WebSiteMapPath("Автотовары","Оборудование и тюнинг"),
new WebSiteMapPath("Бытовая техника","Аксессуары для бытовой техники"),
new WebSiteMapPath("Бытовая техника","Бытовая химия"),
new WebSiteMapPath("Дом, дача и ремонт","Биотуалеты"),
new WebSiteMapPath("Дом, дача и ремонт","Ванная комната"),
new WebSiteMapPath("Дом, дача и ремонт","Дача и сад"),
new WebSiteMapPath("Дом, дача и ремонт","Домашний текстиль"),
new WebSiteMapPath("Дом, дача и ремонт","Инженерная сантехника"),
new WebSiteMapPath("Дом, дача и ремонт","Инструмент"),
new WebSiteMapPath("Дом, дача и ремонт","Интерьер"),
new WebSiteMapPath("Дом, дача и ремонт","Кухня"),
new WebSiteMapPath("Дом, дача и ремонт","Напольные покрытия"),
new WebSiteMapPath("Дом, дача и ремонт","Освещение"),
new WebSiteMapPath("Дом, дача и ремонт","Отопление и вентиляция"),
new WebSiteMapPath("Дом, дача и ремонт","Теплые полы"),
new WebSiteMapPath("Дом, дача и ремонт","Товары для животных"),
new WebSiteMapPath("Дом, дача и ремонт","Хозяйственные товары"),
new WebSiteMapPath("Дом, дача и ремонт","Электрика и кабель"),
new WebSiteMapPath("Дом, дача и ремонт","Бытовая химия"),
new WebSiteMapPath("Дом, дача и ремонт","Дверная фурнитура"),
new WebSiteMapPath("Дом, дача и ремонт","Новый год"),
new WebSiteMapPath("Дом, дача и ремонт","Сейфы"),
new WebSiteMapPath("Дом, дача и ремонт","Снегоуборщики"),
new WebSiteMapPath("Комплектующие","Кабели, шлейфы и переходники"),
new WebSiteMapPath("Комплектующие","Серверное оборудование"),
new WebSiteMapPath("Комплектующие","Программное обеспечение"),
new WebSiteMapPath("Компьютеры и ноутбуки","Программное обеспечение"),
new WebSiteMapPath("Офис и сети","Банковское оборудование"),
new WebSiteMapPath("Офис и сети","Торговое оборудование"), 
new WebSiteMapPath("Офис и сети","Программное обеспечение"), 
new WebSiteMapPath("ТВ, Аудио, Видео","DJ оборудование"),
new WebSiteMapPath("Фото и видеокамеры","Студийное оборудование"),
new WebSiteMapPath("Цифровые книги и музыка"), 
new WebSiteMapPath("Товары для животных"),
new WebSiteMapPath("Парфюмерия и косметика"),  
new WebSiteMapPath("Новогодние подарки и товары"), 
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Ю-Gift – идеи подарков",
                    "Автозапчасти",
                    "Парфюмерия и косметика",
                    "Товары для животных",
                    "Услуги",
                    "Товары к Новому году",
                    "Детские товары",
                    "Туризм, Спорт, Вело",
                    "Сервисы",
                    "Авиабилеты",
                    "Мебель",
                    "Подарочные сертификаты",
                    "Подарочная упаковка",
                    "Гаджеты и подарки",
                    "Подбор автотоваров",
                    "Суперцена",
                    "Подбор багажных систем"
                };
    }
}