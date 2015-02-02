using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.ElectrovenikRu
{
    internal static class ElectrovenikRuConstants
    {
        public const int Id = 621;

        public const string Name = "Electrovenik.Ru";

        public const string SiteUri = "http://www.electrovenik.ru/";

        public const string UriMask = "http://{0}.electrovenik.ru/";

        public const bool Articles = true;

        public static readonly WebCity[] Cities = ElectrovenikRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ShopsAvailability = false;

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Всё для авто", "Автосигнализации"),
                    new WebSiteMapPath("Всё для авто", "Шины"),
                    new WebSiteMapPath("Всё для авто", "Шины", "Зимние"),
                    new WebSiteMapPath("Всё для авто", "Шины", "Летние"),
                    new WebSiteMapPath("Встраиваемая бытовая техника", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Встраиваемая бытовая техника", "Измельчители пищевых отходов"),
                    new WebSiteMapPath("Встраиваемая бытовая техника", "Подогреватели посуды"),
                    new WebSiteMapPath("Климатическое оборудование", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Комплектующие",
                                       "Корпуса и док-станции для жестких дисков"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Комплектующие", "Кулеры и системы охлаждения"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Компьютерные аксессуары", "USB-концентраторы"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Компьютерные аксессуары", "Подставки для ноутбуков"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Мебель"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Мебель", "Компьютерные столы"),
                    new WebSiteMapPath("Компьютеры и ноутбуки", "Мебель", "Кресла и стулья"),
                    new WebSiteMapPath("Крупная бытовая техника", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Мелкая бытовая техника", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Мелкая кухонная техника", "Перцемолки"),
                    new WebSiteMapPath("Мелкая кухонная техника", "Системы розлива пива"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для планшетов", "Держатели"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для планшетов", "Защитные пленки"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для планшетов", "Стилусы"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для планшетов", "Чистящие средства"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для смартфонов", "Держатели"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для смартфонов", "Защитные пленки"),
                    new WebSiteMapPath("Планшеты и Смартфоны", "Аксессуары для смартфонов",
                                       "Перчатки для сенсорных экранов"),
                    new WebSiteMapPath("Приборы для индивидуального ухода", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Телевизоры, аудио, видео", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Телевизоры, аудио, видео", "Аксессуары и средства ухода",
                                       "WiFi адаптеры для телевизоров"),
                    new WebSiteMapPath("Телевизоры, аудио, видео", "Аксессуары и средства ухода", "Средства по уходу"),
                    new WebSiteMapPath("Уборочная техника и оборудование", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Уборочная техника и оборудование", "Насосы"),
                    new WebSiteMapPath("Уборочная техника и оборудование", "Насосы", "Мотопомпы"),
                    new WebSiteMapPath("Уборочная техника и оборудование", "Насосы", "Насосы поверхностные"),
                    new WebSiteMapPath("Уборочная техника и оборудование", "Насосы", "Насосы погружные"),
                    new WebSiteMapPath("Цифровая техника", "Аксессуары и средства ухода"),
                    new WebSiteMapPath("Цифровая техника", "Фотоаппараты и принадлежности", "Светофильтры")

                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Инструменты",
                    "Все для дома",
                    "Детские товары",
                    "Садовая техника и инвентарь",
                    "Спорт и отдых",
                    "Банковское оборудование",
                    "Уцененный товар",
                    "Сантехника"
                };
    }
}