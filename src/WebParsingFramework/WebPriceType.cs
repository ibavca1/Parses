using System;

namespace WebParsingFramework
{
    /// <summary>
    /// Тип интернет-цен.
    /// </summary>
    [Flags]
    public enum WebPriceType
    {
        /// <summary>
        /// Неопределенная
        /// </summary>
        None = 0,

        /// <summary>
        /// Онлайн-цена.
        /// </summary>
        Internet = 1,

        /// <summary>
        /// Розничная цена.
        /// </summary>
        Retail = 2,

        /// <summary>
        /// Все цены.
        /// </summary>
        All = Internet | Retail
    }
}