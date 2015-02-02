using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.JustRu
{
    internal static class JustRuConstants
    {
        public const int Id = 617;

        public const string Name = "Just.Ru";

        public const string SiteUri = "http://www.just.ru/";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = JustRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Apple", "Литература для Apple"),
                    new WebSiteMapPath("Apple", "Программы для Mac"),
                    new WebSiteMapPath("Apple", "Сервис Apple"),
                    new WebSiteMapPath("Авто", "Автокресла"),
                    new WebSiteMapPath("Авто", "Автосигнализации"),
                    new WebSiteMapPath("Авто", "Диски"),
                    new WebSiteMapPath("Авто", "Компрессоры"),
                    new WebSiteMapPath("Авто", "Пускозарядные устройcтва"),
                    new WebSiteMapPath("Авто", "Шины"),
                    new WebSiteMapPath("Железо", "Контроллеры"),
                    new WebSiteMapPath("Железо", "Охлаждение"),
                    new WebSiteMapPath("Игры", "Nintendo"),
                    new WebSiteMapPath("Игры", "PlayStation 3"),
                    new WebSiteMapPath("Игры", "PSP и Vita"),
                    new WebSiteMapPath("Игры", "Xbox 360"),
                    new WebSiteMapPath("Игры", "Приставки на ОС Android и 32-bit"),
                    new WebSiteMapPath("Офис", "Банковское оборудование"),
                    new WebSiteMapPath("Офис", "Кабели и шлейфы"),
                    new WebSiteMapPath("Офис", "Кресла, стулья, столы"),
                    new WebSiteMapPath("Офис", "Канцелярия"),
                    new WebSiteMapPath("Компы ноуты", "Программное обеспечение")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "JUST.Уценка"
                };
    }
}