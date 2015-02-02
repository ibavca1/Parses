using System;

namespace WebParsingFramework
{
    /// <summary>
    /// Метаданные веб-сайта.
    /// </summary>
    public class WebSiteMetadata
    {
        public bool ProductActicle { get; set; }

        /// <summary>
        /// Типы цен.
        /// </summary>
        public WebPriceType PriceTypes { get; set; }

        /// <summary>
        /// Наличие в магазинах.
        /// </summary>
        public bool AvailabilityInShops { get; set; }

        /// <summary>
        /// Фильтр страниц.
        /// </summary>
        public WebPagesFilter PagesFilter { get; set; }

        /// <summary>
        /// Тайм-айт между запросами с одного прокси.
        /// </summary>
        public TimeSpan ProxyTimeout { get; set; }
    }
}