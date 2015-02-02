using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.OldiRu
{
    internal static class OldiRuConstants
    {
        public const int Id = 612;

        public const string Name = "Oldi.Ru";

        public const string SiteUri = "http://www.oldi.ru";

        public const bool Articles = true;

        public static readonly WebCity[] Cities = OldiRuCity.All.ToArray();

        public const WebPriceType PriceTypes = WebPriceType.Internet | WebPriceType.Retail;

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool ShopsAvailability = true;

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Сетевое оборудование", "Компоненты для монтажа сетей"),
                    new WebSiteMapPath("Комплектующие для компьютера", "Аксессуары для комплектующих"), 
                    new WebSiteMapPath("Программное обеспечение"),
                    new WebSiteMapPath("Офисная техника и расходные материалы","Расходные, чистящие материалы","Чистящие средства"), 
                    new WebSiteMapPath("Офисная техника и расходные материалы","Расходные, чистящие материалы","Расходные материалы для Ламинаторов"), 
                    new WebSiteMapPath("Офисная техника и расходные материалы","Шредеры"), 
                    new WebSiteMapPath("Офисная техника и расходные материалы","Офисная мебель"),
                    new WebSiteMapPath("Офисная техника и расходные материалы","Системы видеонаблюдения","Комплекты сигнализаций + GSM"),
                    new WebSiteMapPath("Офисная техника и расходные материалы","Беспроводные и проводные датчики"),
                    new WebSiteMapPath("Офисная техника и расходные материалы","Муляжи видеокамер"),
                    new WebSiteMapPath("Телевизоры, видео-аудио техника","Внешние жесткие диски, карты памяти, флэшки", "Аксессуары к дискам"),
                    new WebSiteMapPath("Бытовая техника","Свет"), 
                    new WebSiteMapPath("Бытовая техника","Сувениры и подарки"),
                    new WebSiteMapPath("Климатическая техника","Аксессуары"), 
                    new WebSiteMapPath("Товары для дачи и отдыха","Биотуалеты"), 
                    new WebSiteMapPath("Товары для дачи и отдыха","Охранные системы для кемпингов, домов, велосипедов, лодок"), 
                    new WebSiteMapPath("Товары для дачи и отдыха","Средства против комаров и насекомых"), 
                    new WebSiteMapPath("Товары для дачи и отдыха","Товары для пикника"), 
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Весь каталог",
                    "Конфигуратор ПК",
                    "Установка кондиционеров",
                    "Услуги",
                    "Подарочные сертификаты",
                    "Распродажа",
                };
    }
}