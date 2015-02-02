using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.PultRu
{
    internal static class PultRuConstants
    {
        public const int Id = 613;

        public const string Name = "Pult.Ru";

        public const string SiteUri = "http://pult.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = false;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly WebCity[] SupportedCities = PultRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
                    new WebSiteMapPath("Кабели", "Беспроводные передатчики и по витой паре HDMI"),
                    new WebSiteMapPath("Кабели", "Разъемы и переходники"),
                    new WebSiteMapPath("Кабели", "Розетки"),
                    new WebSiteMapPath("Кабели", "Силовые кабели"),
                    new WebSiteMapPath("Наушники", "Аксессуары для наушников"),
                    new WebSiteMapPath("Проигрыватели виниловых дисков", "Аксессуары для виниловых проигрывателей"),
                    new WebSiteMapPath("Проигрыватели виниловых дисков", "Головки звукоснимателя"),
                    new WebSiteMapPath("Проигрыватели виниловых дисков", "Средства по уходу и хранению"),
                    new WebSiteMapPath("Проигрыватели виниловых дисков", "Тонармы"),
                    new WebSiteMapPath("Проигрыватели виниловых дисков", "Фонокорректоры"),
                    new WebSiteMapPath("Система умный дом", "Мультирум"),
                    new WebSiteMapPath("Система умный дом", "Настенные панели"),
                    new WebSiteMapPath("Система умный дом", "Пульты программируемые"),
                    new WebSiteMapPath("Система умный дом", "Управляемые шторы"),
                    new WebSiteMapPath("Студийное оборудование", "Аксессуары"),
                    new WebSiteMapPath("Студийное оборудование", "Световое оборудование"),
                    new WebSiteMapPath("Телевизоры и панели", "Багеты для телевизоров"),
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Мебель",
                    "Кронштейны"
                };
    }
}