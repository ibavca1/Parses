using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.SvyaznoyRu
{
    internal static class SvyaznoyRuConstants
    {
        public const int Id = 59;

        public const string Name = "Svyaznoy.Ru";

        public const string SiteUri = "http://www.svyaznoy.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public const bool SupportArticles = true;

        public const bool SupportAvailabilityInShops = false;

        public static readonly WebCity[] Cities = SvyaznoyRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Аксессуары", "Для фотоаппаратов", "Аксессуары для вспышек"),
                    new WebSiteMapPath("Аксессуары", "Для фотоаппаратов", "Насадки и крышки"),
                    new WebSiteMapPath("Аксессуары", "Для фотоаппаратов", "Объективы"),
                    new WebSiteMapPath("Аксессуары", "Для фотоаппаратов", "Уход за оптикой"),
                    new WebSiteMapPath("Аксессуары", "Для фотоаппаратов", "Фильтры"),
                    new WebSiteMapPath("Аксессуары", "Защитные пленки"),
                    new WebSiteMapPath("Аксессуары", "Защитные пленки", "Защитные пленки для планшетов"),
                    new WebSiteMapPath("Аксессуары", "Защитные пленки", "Защитные пленки для телефонов"),
                    new WebSiteMapPath("Аксессуары", "Компьютерные аксессуары", "USB-концентраторы"),
                    new WebSiteMapPath("Аксессуары", "Компьютерные аксессуары", "Очки для компьютера"),
                    new WebSiteMapPath("Аксессуары", "Компьютерные аксессуары", "Средства для ухода"),
                    new WebSiteMapPath("Аксессуары", "Компьютерные аксессуары", "Стилусы"),
                    new WebSiteMapPath("Аудио- и видеотехника", "Аксессуары", "Защитные пленки"),
                    new WebSiteMapPath("Гаджеты, ридеры, приставки", "Игровые приставки и игры", "Игры для приставок"),
                    new WebSiteMapPath("Гаджеты, ридеры, приставки", "Товары для спорта и здоровья", "Велокомпьютеры"),
                    new WebSiteMapPath("Гаджеты, ридеры, приставки", "Товары для спорта и здоровья",
                                       "Спортивные браслеты"),
                    new WebSiteMapPath("Гаджеты, ридеры, приставки", "Товары для спорта и здоровья", "Спортивные часы"),
                    new WebSiteMapPath("Ноутбуки и компьютеры", "Аксессуары", "Подставки для ноутбуков"),
                    new WebSiteMapPath("Ноутбуки и компьютеры", "Аксессуары", "Средства для ухода"),
                    new WebSiteMapPath("Ноутбуки и компьютеры", "Антивирусы, ПО"),
                    new WebSiteMapPath("Ноутбуки и компьютеры", "Мультимедиа", "Очки для компьютера"),
                    new WebSiteMapPath("Ноутбуки и компьютеры", "Устройства ввода", "Коврики для мыши"),
                    new WebSiteMapPath("Ноутбуки и компьютеры", "Apple"),
                    new WebSiteMapPath("Планшеты", "Аксессуары", "Защитные пленки"),
                    new WebSiteMapPath("Телефоны, связь", "Аксессуары", "Защитные пленки"),
                    new WebSiteMapPath("Фото- и видеокамеры", "Аксессуары", "Защитные пленки"),
                    new WebSiteMapPath("Фото- и видеокамеры", "Вспышки и свет", "Аксессуары для вспышек"),
                    new WebSiteMapPath("Фото- и видеокамеры", "Объективы", "Насадки и крышки"),
                    new WebSiteMapPath("Фото- и видеокамеры", "Объективы", "Уход за оптикой"),
                    new WebSiteMapPath("Фото- и видеокамеры", "Объективы", "Фильтры"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Подбор аксессуаров",
                    "Распродажа",
                    "Мобильный эквайринг SumUp",
                    "Подбери аксессуар к своему устройству",
                    "Чехол с твоим дизайном",
                    "Услуги"
                };
    }
}