using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.KeyRu
{
    internal static class KeyRuConstants
    {
        public const int Id = 67;

        public const string Name = "Key.ru";

        public const string SiteUri = "http://key.ru/";

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public const bool ProductsActicle = true;

        public const bool AvailabilityInShops = true;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(3);

        public static readonly WebCity[] Cities = KeyRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Офисная техника","Банковское оборудование"),
new WebSiteMapPath("Офисная техника","Брошюровщики"),
new WebSiteMapPath("Офисная техника","Уничтожители бумаги"),
new WebSiteMapPath("Программы и Книги"),
new WebSiteMapPath("Программы и Книги","Антивирусы"),
new WebSiteMapPath("Программы и Книги","Бизнес-продукты"),
new WebSiteMapPath("Программы и Книги","Операционные системы"),
new WebSiteMapPath("Программы и Книги","Офисное ПО"),
new WebSiteMapPath("Программы и Книги","Программы для автомобилистов"),
new WebSiteMapPath("Программы и Книги","Программы для навигаторов"),
new WebSiteMapPath("Программы и Книги","Сервисные программы"),
new WebSiteMapPath("Программы и Книги","Средства разработки"),
new WebSiteMapPath("Программы и Книги","Фото, видео и аудио редакторы"),
new WebSiteMapPath("Расходные материалы","Боксы и конверты"),
new WebSiteMapPath("Расходные материалы","Наклейки и этикетки"),
new WebSiteMapPath("Расходные материалы","Пленка для ламинатора"),
new WebSiteMapPath("Расходные материалы","Чистящие средства"),
new WebSiteMapPath("Смартфоны","Объективы для смартфонов"),
new WebSiteMapPath("Спорт и здоровье","Кистевой тренажер"),
new WebSiteMapPath("Сувениры","USB-хабы"),
new WebSiteMapPath("Сувениры","Арома диффузоры"),
new WebSiteMapPath("Сувениры","Сувениры и игрушки"),
new WebSiteMapPath("Сувениры","Фоторамки"),
new WebSiteMapPath("Сувениры","Часы"),
new WebSiteMapPath("Фотоаппараты","Конвертеры"),
new WebSiteMapPath("Фотоаппараты","Крышки и бленды"),
new WebSiteMapPath("Фотоаппараты","Окуляры и насадки"),
new WebSiteMapPath("Фотоаппараты","Ремни"),
new WebSiteMapPath("Фотоаппараты","Фильтры"),
new WebSiteMapPath("Электропитание и кабели","KVM-переключатели"),
new WebSiteMapPath("Электропитание и кабели","USB, IEEE, COM, PS/2 и LPT кабели"),
                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {

                };
    }
}