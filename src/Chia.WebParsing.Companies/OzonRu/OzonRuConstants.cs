using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OzonRu
{
    internal static class OzonRuConstants
    {
        public const int Id = 604;

        public const string Name = "Ozon.Ru";

        public const string SiteUri = "http://www.ozon.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = WebCities.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new WebSiteMapPath[]
                {
new WebSiteMapPath("Бытовая техника", "Освещение и электротовары"),
new WebSiteMapPath("Бытовая техника","Электроинструмент"), 
new WebSiteMapPath("Бытовая техника","Для красоты и здоровья","Зубные щетки и ирригаторы","Насадки для зубных щеток и ирригаторов"),
new WebSiteMapPath("Бытовая техника","Кухонная техника","Кофеварки и кофемолки","Аксессуары для кофемашин и кофеварок"),
new WebSiteMapPath("Бытовая техника","Кухонная техника","Кофеварки и кофемолки","Вспениватель молока"),
new WebSiteMapPath("Бытовая техника","Кухонная техника","Кофеварки и кофемолки","Капсулы для кофемашин"),
new WebSiteMapPath("Бытовая техника","Техника для дома","Безопасность и наблюдение","Домофоны"),
new WebSiteMapPath("Бытовая техника","Техника для дома","Безопасность и наблюдение","Охранное оборудование для дома и дачи"),
new WebSiteMapPath("Электроника","Аксессуары","Кабели и переходники","USB-хабы (разветвители)"),
new WebSiteMapPath("Электроника","Аксессуары","Кабели и переходники","Дата-кабели"),
new WebSiteMapPath("Электроника","Аксессуары","Кабели и переходники","Кабели"),
new WebSiteMapPath("Электроника","Аксессуары","Кабели и переходники","Переходники"),
new WebSiteMapPath("Электроника","Аксессуары","Клавиатуры, мыши, джойстики и рули","Коврики для мыши"),
new WebSiteMapPath("Электроника","Аксессуары","Очки","Очки для компьютера"),
new WebSiteMapPath("Электроника","Безопасность и видеонаблюдение","GPS - трекеры"),
new WebSiteMapPath("Электроника","Безопасность и видеонаблюдение","Домофоны"),
new WebSiteMapPath("Электроника","Безопасность и видеонаблюдение","Звонки"),
new WebSiteMapPath("Электроника","Безопасность и видеонаблюдение","Охранное оборудование для дома и дачи"),
new WebSiteMapPath("Электроника","Игровые приставки","Comfy","Игра для Easy PC"),
new WebSiteMapPath("Электроника","Игровые приставки","Nintendo Wii","Виниловые наклейки"),
new WebSiteMapPath("Электроника","Игровые приставки","Sony PlayStation 2","Виниловые наклейки"),
new WebSiteMapPath("Электроника","Игровые приставки","Sony PlayStation 3","Виниловые наклейки"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Подарочные сертификаты",
                    "Детям и мамам",
                    "Книги",
                    "Софт и игры",
                    "DVD и Blu-ray",
                    "Музыка",
                    "Дом, сад, зоотовары",
                    "Спорт и отдых",
                    "Красота и здоровье",
                    "Одежда, обувь, аксессуары",
                    "Кожгалантерея, часы, багаж",
                    "Антиквариат, винтаж, искусство",
                    "Авиабилеты и Ж/Д билеты",
                    "OZON.digital",
                    "LUXE",
                    "Уцененная электроника"
                };
    }
}