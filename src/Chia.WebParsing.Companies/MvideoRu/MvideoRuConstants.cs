using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.MvideoRu
{
    public class MvideoRuConstants
    {
        public const int Id = 38;

        public const string Name = "Mvideo.Ru";

        public const string SiteUri = "http://www.mvideo.ru";

        public const string SiteUriMask = "http://{0}.mvideo.ru";

        public const bool AvailabilityInShops = true;

        public const bool ProductArticle = true;

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(3);

        public static readonly WebCity[] Cities = MvideoRuCity.All.ToArray();

        public static string prefix = "";

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Авто техника","Навигаторы и автомобильная электроника","Аксессуары для GPS навигаторов"),
new WebSiteMapPath("Авто техника","Навигаторы и автомобильная электроника","Карты для навигаторов"),
new WebSiteMapPath("Аудио техника","Аудиодиски"),
new WebSiteMapPath("Аудио техника","Оборудование для диджеев","Аксессуары для DJ оборудования"),
new WebSiteMapPath("Встраиваемая техника","Встраиваемая бытовая техника","Аксессуары для встраиваемой техники"),
new WebSiteMapPath("Игры и развлечения","Blu-ray / DVD диски"),
new WebSiteMapPath("Игры и развлечения","Blu-ray / DVD диски","Blu-ray диски"),
new WebSiteMapPath("Игры и развлечения","Blu-ray / DVD диски","DVD диски"),
new WebSiteMapPath("Игры и развлечения","Игры для игровых приставок"),
new WebSiteMapPath("Игры и развлечения","Игры для игровых приставок","Игры для Nintendo"),
new WebSiteMapPath("Игры и развлечения","Игры для игровых приставок","Игры для Sony Playstation"),
new WebSiteMapPath("Игры и развлечения","Игры для игровых приставок","Игры для XBOX 360"),
new WebSiteMapPath("Игры и развлечения","Игры для игровых приставок","Игры для XBOX ONE"),
new WebSiteMapPath("Игры и развлечения","Компьютерные программы и PC игры"),
new WebSiteMapPath("Игры и развлечения","Компьютерные программы и PC игры","Антивирусы"),
new WebSiteMapPath("Игры и развлечения","Компьютерные программы и PC игры","Другое программное обеспечение"),
new WebSiteMapPath("Игры и развлечения","Компьютерные программы и PC игры","Игры для ПК"),
new WebSiteMapPath("Игры и развлечения","Компьютерные программы и PC игры","Программы Microsoft Office и Windows"),
new WebSiteMapPath("Игры и развлечения","Музыкальные инструменты","Аксессуары для музыкальных инструментов"),
new WebSiteMapPath("Красота и здоровье","Товары для детей","Посуда для детей"),
new WebSiteMapPath("Красота и здоровье","Товары для здоровья","Часы спортивные"),
new WebSiteMapPath("Красота и здоровье","Товары для спорта"),
new WebSiteMapPath("Ноутбуки, планшеты и компьютеры","Компьютерные аксессуары","Очки для компьютера"),
new WebSiteMapPath("Телевизоры и видео","Аксессуары для телевизоров","Чистящие средства для экранов"),
new WebSiteMapPath("Телефоны","Аксессуары для телефонов","Защитные пленки для телефонов"),
new WebSiteMapPath("Телефоны","Аксессуары для телефонов","Объективы для смартфонов"),
new WebSiteMapPath("Телефоны","Гаджеты","Фитнес-трекеры"),
new WebSiteMapPath("Техника для дома","Аксессуары для дома","Прочие хозяйственные товары"),
new WebSiteMapPath("Техника для дома","Бытовая техника","Гладильные доски"),
new WebSiteMapPath("Техника для дома","Бытовая техника","Часы"),
new WebSiteMapPath("Техника для дома","Освещение","Лампы"),
new WebSiteMapPath("Техника для дома","Освещение","Светильники"),
new WebSiteMapPath("Техника для дома","Освещение","Электронные свечи"),
new WebSiteMapPath("Техника для дома","Системы безопасности","Муляжи систем видеонаблюдения"),
new WebSiteMapPath("Техника для дома","Системы безопасности","Охранные системы и сигнализации"),
new WebSiteMapPath("Техника для кухни","Аксессуары для крупной кухонной техники"),
new WebSiteMapPath("Техника для кухни","Аксессуары для крупной кухонной техники","Аксессуары для кухонных плит"),
new WebSiteMapPath("Техника для кухни","Аксессуары для крупной кухонной техники","Аксессуары для микроволовых печей"),
new WebSiteMapPath("Техника для кухни","Аксессуары для крупной кухонной техники","Аксессуары для посудомоечных машин"),
new WebSiteMapPath("Техника для кухни","Аксессуары для крупной кухонной техники","Аксессуары для холодильников"),
new WebSiteMapPath("Техника для кухни","Аксессуары для кухонной техники"),
new WebSiteMapPath("Техника для кухни","Аксессуары для кухонной техники","Насадки и наборы"),
new WebSiteMapPath("Техника для кухни","Аксессуары для кухонной техники","Чистящие средства"),
new WebSiteMapPath("Техника для кухни","Посуда","Кухонные ножи и доски"),
new WebSiteMapPath("Техника для кухни","Посуда","Прочие кухонные аксессуары"),
new WebSiteMapPath("Техника для кухни","Посуда","Формы для выпекания"),
new WebSiteMapPath("Техника для кухни","Приготовление кофе","Аксессуары для кофемашин"),
new WebSiteMapPath("Техника для кухни","Приготовление кофе","Кофе"),
new WebSiteMapPath("Фото и видео","Аксессуары для фото и видеотехники","Светофильтры"),
new WebSiteMapPath("Фото и видео","Аксессуары для фото и видеотехники","Чистящие средства для оптики"),
new WebSiteMapPath("Фото и видео","Аксессуары для фото и видеотехники","Аксессуары для фотоаппаратов Leica"), // входит в другие, корявая разметка
new WebSiteMapPath("Фото и видео","Фотоаппараты премиум класса"), // входит в другие, корявая разметка
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Акции",
                    "Gorenje коллекция Pininfarina",
                    "Gorenje коллекция Karim Rashid",
                    "Gorenje коллекция Classico",
                    "Siemens коллекция Aviator"
                };
    }
}