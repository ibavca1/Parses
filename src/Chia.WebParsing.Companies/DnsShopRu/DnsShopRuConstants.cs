using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.DnsShopRu
{
    internal static class DnsShopRuConstants
    {
        public const int Id = 351;

        public const string Name = "DNS";

        public const string SiteUri = "http://www.dns-shop.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ProductsArticles = true;

        public const bool AvailabilityInShops = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly DnsShopRuCity[] SupportedCities = DnsShopRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Автотовары","Автозвук","Кабель и установочные комплекты"),
new WebSiteMapPath("Автотовары","Автозвук","Конденсаторы"),
new WebSiteMapPath("Автотовары","Навигация","Чехлы"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Игры"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Портативные игровые консоли и аксессуары","Чехлы"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Радиоуправляемые игрушки","Летающая техника"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Радиоуправляемые игрушки","Машины"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Радиоуправляемые игрушки","Танки"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Сувенирная продукция","Брелоки"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Сувенирная продукция","Одежда"),
new WebSiteMapPath("Игры, консоли и манипуляторы","Сувенирная продукция","Прочее"),
new WebSiteMapPath("Климат и техника для дома","Аксессуары"),
new WebSiteMapPath("Климат и техника для дома","Осветительные приборы и источники света","Светильники декоративные"),
new WebSiteMapPath("Климат и техника для дома","Осветительные приборы и источники света","Светильники настольные"),
new WebSiteMapPath("Климат и техника для дома","Осветительные приборы и источники света","Энергосберегающие и светодиодные лампы"),
new WebSiteMapPath("Комплектующие к ПК","Кулеры и системы охлаждения"),
new WebSiteMapPath("Комплектующие к ПК","Моддинг"),
new WebSiteMapPath("Красота и здоровье","Аксессуары"),
new WebSiteMapPath("Ноутбуки, компьютеры и программное обеспечение","Аксессуары для ноутбуков","Защитные пленки"),
new WebSiteMapPath("Ноутбуки, компьютеры и программное обеспечение","Аксессуары для ноутбуков","Системы охлаждения"),
new WebSiteMapPath("Ноутбуки, компьютеры и программное обеспечение","Программное обеспечение"),
new WebSiteMapPath("Периферия и оргтехника","Коврики"),
new WebSiteMapPath("Периферия и оргтехника","Мыши и клавиатуры","Наклейки на клавиатуру"),
new WebSiteMapPath("Периферия и оргтехника","Оргтехника","Детекторы и счетчики банкнот"),
new WebSiteMapPath("Периферия и оргтехника","Оргтехника","Мини АТС, системные телефоны"),
new WebSiteMapPath("Периферия и оргтехника","Оргтехника","Принтеры этикеток, сканеры штрих-кода"),
new WebSiteMapPath("Планшеты, электронные книги, переводчики, фоторамки","Аксессуары для планшетов","Защитные пленки"),
new WebSiteMapPath("Планшеты, электронные книги, переводчики, фоторамки","Аксессуары для планшетов","Перчатки для емкостных экранов"),
new WebSiteMapPath("Приготовление и хранение продуктов","Аксессуары"),
new WebSiteMapPath("Расходные материалы","Запчасти (фотобарабаны, валы)"),
new WebSiteMapPath("Расходные материалы","Картриджи","Расходные материалы для 3D-принтеров"),
new WebSiteMapPath("Расходные материалы","Термопленки"),
new WebSiteMapPath("Расходные материалы","Чистящие средства"),
new WebSiteMapPath("Сетевое оборудование","Инструмент"),
new WebSiteMapPath("Сетевое оборудование","Переключатели и разветвители"),
new WebSiteMapPath("Сетевое оборудование","Розетки, разъемы, кабели"),
new WebSiteMapPath("Смартфоны и сотовые телефоны","Аксессуары для телефонов","Браслеты, умные часы"),
new WebSiteMapPath("Смартфоны и сотовые телефоны","Аксессуары для телефонов","Защитные пленки"),
new WebSiteMapPath("Смартфоны и сотовые телефоны","Аксессуары для телефонов","Радиоуправляемые игрушки"),
new WebSiteMapPath("Смартфоны и сотовые телефоны","Аксессуары для телефонов","Смарт линзы"),
new WebSiteMapPath("Смартфоны и сотовые телефоны","Аксессуары для телефонов","Стилусы"),
new WebSiteMapPath("Уход за одеждой и помещениями","Аксессуары"),
new WebSiteMapPath("Фото и видеокамеры","Аксессуары для фотоаппаратов","Бленды"),
new WebSiteMapPath("Фото и видеокамеры","Аксессуары для фотоаппаратов","Защитные крышки"),
new WebSiteMapPath("Фото и видеокамеры","Аксессуары для фотоаппаратов","Защитные пленки"),
new WebSiteMapPath("Фото и видеокамеры","Аксессуары для фотоаппаратов","Рассеиватели для вспышек"),
new WebSiteMapPath("Фото и видеокамеры","Аксессуары для фотоаппаратов","Фотофильтры"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                };
    }
}