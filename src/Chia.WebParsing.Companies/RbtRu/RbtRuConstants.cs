using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.RbtRu
{
    internal static class RbtRuConstants
    {
        public const int Id = 65;

        public const string Name = "RBT.Ru";

        public const string SiteUri = "http://www.rbt.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(10);

        public const bool SupportArticles = false;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly RbtRuCity[] SupportedCities = RbtRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Автомобильная электроника","Автоковрики"),
new WebSiteMapPath("Автомобильная электроника","Автокосметика"),
new WebSiteMapPath("Автомобильная электроника","Комплекты подключения автозвука"),
new WebSiteMapPath("Автомобильная электроника","Сигнализации автомобильные"),
new WebSiteMapPath("Автомобильная электроника","Щетки и скребки"),
new WebSiteMapPath("Активный отдых","Аксессуары для бассейнов"),
new WebSiteMapPath("Активный отдых","Аксессуары для надувной мебели"),
new WebSiteMapPath("Активный отдых","Бассейны"),
new WebSiteMapPath("Активный отдых","Корзины и наборы для пикника"),
new WebSiteMapPath("Активный отдых","Мангалы и барбекю"),
new WebSiteMapPath("Активный отдых","Мебель для кемпинга"),
new WebSiteMapPath("Активный отдых","Надувная мебель"),
new WebSiteMapPath("Активный отдых","Подушки и наборы для отдыха"),
new WebSiteMapPath("Активный отдых","Посуда для туризма"),
new WebSiteMapPath("Активный отдых","Чемоданы"),
new WebSiteMapPath("Встраиваемая техника","Аксессуары для духовых шкафов"),
new WebSiteMapPath("Встраиваемая техника","Аксессуары для поверхностей"),
new WebSiteMapPath("Встраиваемая техника","Измельчители пищевых отходов"),
new WebSiteMapPath("Инструменты для дома"),
new WebSiteMapPath("Климатическая техника","Аксессуары для климатической техники"),
new WebSiteMapPath("Компьютеры и оргтехника","Кейсы для жестких дисков"),
new WebSiteMapPath("Компьютеры и оргтехника","Компьютерные программы"),
new WebSiteMapPath("Компьютеры и оргтехника","Контроллеры и картридеры"),
new WebSiteMapPath("Компьютеры и оргтехника","Кресла"),
new WebSiteMapPath("Компьютеры и оргтехника","Освещение"),
new WebSiteMapPath("Компьютеры и оргтехника","Очки для компьютера"),
new WebSiteMapPath("Компьютеры и оргтехника","Столы компьютерные"),
new WebSiteMapPath("Компьютеры и оргтехника","Чистящие средства"),
new WebSiteMapPath("Кухонная техника","Аккумуляторы холода"),
new WebSiteMapPath("Кухонная техника","Аксессуары для мясорубок"),
new WebSiteMapPath("Кухонная техника","Аксессуары для плит"),
new WebSiteMapPath("Кухонная техника","Аксессуары и моющие для ПММ"),
new WebSiteMapPath("Кухонная техника","Аксессуары и моющие для чайников"),
new WebSiteMapPath("Кухонная техника","Аксессуары к холодильникам"),
new WebSiteMapPath("Кухонная техника","Кухонные принадлежности"),
new WebSiteMapPath("Кухонная техника","Наборы столовых приборов"),
new WebSiteMapPath("Кухонная техника","Ножи"),
new WebSiteMapPath("Кухонная техника","Посуда для напитков"),
new WebSiteMapPath("Кухонная техника","Проращиватели семян"),
new WebSiteMapPath("Кухонная техника","Сервировка"),
new WebSiteMapPath("Кухонная техника","Фильтры для кофеварок"),
new WebSiteMapPath("Кухонная техника","Хранение продуктов"),
new WebSiteMapPath("Отдых и Развлечения","Игры для игровых приставок"),
new WebSiteMapPath("Приборы персонального ухода","Аксессуары для бритв"),
new WebSiteMapPath("Приборы персонального ухода","Насадки для зубных щеток"),
new WebSiteMapPath("Садовая техника"),
new WebSiteMapPath("Теле-видео-аудио","Чехлы для пультов"),
new WebSiteMapPath("Техника для дома","Аксессуары  для швейных машин"),
new WebSiteMapPath("Техника для дома","Аксессуары для пылесосов"),
new WebSiteMapPath("Техника для дома","Аксессуары для стирки и хранения одежды"),
new WebSiteMapPath("Техника для дома","Аксессуары для утюгов"),
new WebSiteMapPath("Техника для дома","Беспроводные дверные звонки"),
new WebSiteMapPath("Техника для дома","Домашние охранные системы"),
new WebSiteMapPath("Техника для дома","Коврики для ванной"),
new WebSiteMapPath("Техника для дома","Лампочки"),
new WebSiteMapPath("Техника для дома","Светильники"),
new WebSiteMapPath("Техника для дома","Сушилки для белья"),
new WebSiteMapPath("Техника для дома","Товары для уборки"),
new WebSiteMapPath("Товары для детей"),
new WebSiteMapPath("Цифровые устройства","Пленки для телефонов"),

                };

        public static readonly string[] ExcludeKeywords =
            new string[]
                {
                    "Подарочные карты"
                };
    }
}