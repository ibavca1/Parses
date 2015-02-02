namespace WebParsingFramework
{
    /// <summary>
    /// Тип веб-страницы.
    /// </summary>
    public enum WebPageType
    {
        /// <summary>
        /// Неизвестно
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Главная.
        /// </summary>
        Main = 1,

        /// <summary>
        /// Страница товара.
        /// </summary>
        Product = 2,

        /// <summary>
        /// Список товаров.
        /// </summary>
        Catalog = 3,

        /// <summary>
        /// Раздел.
        /// </summary>
        Razdel = 4,

        /// <summary>
        /// Список разделов.
        /// </summary>
        RazdelsList = 5,

        /// <summary>
        /// Страница розничного магазина.
        /// </summary>
        Shop = 6,

        /// <summary>
        /// Список розничных магазинов
        /// </summary>
        ShopsList = 7
    }
}