using System;
using System.Linq;
using WebParsingFramework;

namespace Chia.WebParsing.Companies.TehnoshokRu
{
    internal static class TehnoshokRuConstants
    {
        public const int Id = 119;

        public const string Name = "Tehnoshok.Ru";

        public const string UriMask = "http://{0}.tshok.ru";

        public const string SiteUri = "http://tshok.ru";

        public static readonly TimeSpan ProxyTimeout = TimeSpan.FromSeconds(5);

        public const bool SupportArticles = true;

        public const bool SupportShopsAvailability = true;

        public const WebPriceType SupportedPriceTypes = WebPriceType.Internet;

        public static readonly TehnoshokRuCity[] SupportedCities = TehnoshokRuCity.All.ToArray();

        public static readonly WebSiteMapPath[] ExcludePaths =
            new[]
                {
new WebSiteMapPath("Авто","Автоинверторы"),
new WebSiteMapPath("Авто","Автохимия"),
new WebSiteMapPath("Авто","Противоугонные комплексы"),
new WebSiteMapPath("Авто","Противоугонные комплексы","Автосигнализация"),
new WebSiteMapPath("Авто","Противоугонные комплексы","Брелок для сигнализации"),
new WebSiteMapPath("Авто","Противоугонные комплексы","Иммобилайзер"),
new WebSiteMapPath("Крупная бытовая техника"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Аккумуляторы холода"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Для вытяжек"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Для духовых шкафов"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Для посудомоечных машин"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Для стиральных машин"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Для холодильников"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Салфетки"),
new WebSiteMapPath("Крупная бытовая техника","Аксессуары","Сушилки для белья"),
new WebSiteMapPath("Кухонные принадлежности","Кухонные Аксессуары"),
new WebSiteMapPath("Кухонные принадлежности","Кухонные доски"),
new WebSiteMapPath("Кухонные принадлежности","Мойки кухонные"),
new WebSiteMapPath("Кухонные принадлежности","Мойки кухонные","Керамогранитные"),
new WebSiteMapPath("Кухонные принадлежности","Мойки кухонные","Нержавеющая сталь"),
new WebSiteMapPath("Кухонные принадлежности","Наборы ножей"),
new WebSiteMapPath("Кухонные принадлежности","Овощерезки"),
new WebSiteMapPath("Кухонные принадлежности","Овощечистки"),
new WebSiteMapPath("Кухонные принадлежности","Посуда","Пластиковая посуда"),
new WebSiteMapPath("Кухонные принадлежности","Посуда","Посуда для СВЧ"),
new WebSiteMapPath("Кухонные принадлежности","Посуда","Турки"),
new WebSiteMapPath("Кухонные принадлежности","Посуда","Формы"),
new WebSiteMapPath("Кухонные принадлежности","Посуда","Чайники"),
new WebSiteMapPath("Кухонные принадлежности","Ручные измельчители"),
new WebSiteMapPath("Кухонные принадлежности","Сырорезки"),
new WebSiteMapPath("Кухонные принадлежности","Тёрки"),
new WebSiteMapPath("Кухонные принадлежности","Френч-прессы"),
new WebSiteMapPath("Кухонные принадлежности","Хлебницы"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К бритвам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К воздухоочистителям"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К гладильным доскам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К кофеваркам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К кух. комбайнам и соковыжималкам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К микроволновым печам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К мультиваркам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К мясорубкам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К пылесосам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К пылесосам","Моющие средства"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К пылесосам","Насадки для пылесоса"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К пылесосам","Фильтры для пылесоса"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К утюгам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К электр. зуб. щеткам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","К эпиляторам"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","Новогодние украшения"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","Специальные средства"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","Сушилки для обуви"),
new WebSiteMapPath("Малая бытовая техника","Аксессуары","Электролампы"),
new WebSiteMapPath("Малая бытовая техника","Красота и здоровье","Косметические приборы","Зеркала косметические"),
new WebSiteMapPath("Малая бытовая техника","Техника для дома","Гладильные доски"),
new WebSiteMapPath("Малая бытовая техника","Техника для дома","Мебель"),
new WebSiteMapPath("Малая бытовая техника","Техника для дома","Мебель","Стулья"),
new WebSiteMapPath("Малая бытовая техника","Техника для дома","Светильники"),
new WebSiteMapPath("Ноутбуки и ПК","Аксессуары и расходные материалы","Для ноутбуков","Замки для ноутбуков"),
new WebSiteMapPath("Ноутбуки и ПК","Аксессуары и расходные материалы","Для ноутбуков","Подставки"),
new WebSiteMapPath("Ноутбуки и ПК","Аксессуары и расходные материалы","Для ноутбуков","Чистящие средства"),
new WebSiteMapPath("Ноутбуки и ПК","Аксессуары и расходные материалы","Коврики для мышек"),
new WebSiteMapPath("Ноутбуки и ПК","Аксессуары и расходные материалы","Программное обеспечение"),
new WebSiteMapPath("Телевизоры, аудио, видео","Аксессуары","Медиапродукция"),
new WebSiteMapPath("Телевизоры, аудио, видео","Аксессуары","Хранение дисков"),
new WebSiteMapPath("Телефоны","Аксессуары","Защитные плёнки"),
new WebSiteMapPath("Телефоны","Аксессуары","Универсальные коврики"),
new WebSiteMapPath("Фото и видеокамеры","Аксессуары","Светофильтр")
                };

        public static readonly string[] ExcludeKeywords =
            new[]
                {
                    "Товары для спорта, дачи и туризма",
                    "Игры",
                    "Часы",
                    "Строительный инструмент"
                };
    }
}