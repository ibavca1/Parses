using System;
using System.Diagnostics.Contracts;

namespace WebParsingFramework
{
    /// <summary>
    /// Контент веб-страницы, основанный на строке.
    /// </summary>
    public class StringWebPageContent : WebPageContent
    {
        private readonly string _content;

        /// <summary>
        /// Создает новый экземпляр класса <see cref="StringWebPageContent"/>.
        /// </summary>
        protected StringWebPageContent()
        {
        }

        /// <summary>
        /// Создает новый экземпляр класса <see cref="StringWebPageContent"/>.
        /// </summary>
        /// <param name="content">Контент в виде строки.</param>
        public StringWebPageContent(string content)
        {
            Contract.Requires<ArgumentNullException>(content != null, "content");

            _content = content;
        }

        /// <summary>
        /// Возвращает содержимое в виде строки.
        /// </summary>
        protected virtual string Content
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);
                return _content;
            }
        }

        /// <summary>
        /// Возвращает содержимое в виде массива байтов.
        /// </summary>
        /// <returns>Содержимое в виде массива байтов.</returns>
        public override byte[] ReadAsByteArray()
        {
            return Encoding.GetBytes(Content);
        }

        /// <summary>
        /// Возвращает содержимое в виде строки.
        /// </summary>
        /// <returns>Содержимое в виде строки.</returns>
        public override string ReadAsString()
        {
            return Content;
        }
    }
}