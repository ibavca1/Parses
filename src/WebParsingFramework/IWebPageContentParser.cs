using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    /// <summary>
    /// Парсер содержимого веб-страницы.
    /// </summary>
    [ContractClass(typeof (WebPageContentParserContract))]
    public interface IWebPageContentParser
    {
        /// <summary>
        /// Производит разбор контента веб-страницы.
        /// </summary>
        /// <param name="page">Экземпляр страницы, разбор которой производится.</param>
        /// <param name="content">Контент страницы, разбор которого производится.</param>
        /// <param name="context">Контекст, в рамках которого производится разбор.</param>
        /// <returns>Результат разбора.</returns>
        WebPageContentParsingResult Parse(WebPage page, WebPageContent content, WebPageContentParsingContext context);

        /// <summary>
        /// Производит асинхронный разбор контента веб-страницы.
        /// </summary>
        /// <param name="page">Экземпляр страницы, разбор которой производится.</param>
        /// <param name="content">Контент страницы, разбор которого производится.</param>
        /// <param name="context">Контекст, в рамках которого производится разбор.</param>
        /// <param name="cancellationToken">Токен <see cref="CancellationToken"/>, который будет назначен задаче.</param>
        /// <returns>Задача, содержащая результат разбора.</returns>
        Task<WebPageContentParsingResult> ParseAsync(WebPage page, WebPageContent content,
            WebPageContentParsingContext context, CancellationToken cancellationToken);
    }

    /// <summary>
    /// Парсер содержимого веб-страницы.
    /// </summary>
    [ContractClass(typeof(WebPageContentParserContract2<>))]
    public interface IWebPageContentParser<out TPageType> : IWebPageContentParser
        where TPageType : struct 
    {
        /// <summary>
        /// Тип веб-страницы.
        /// </summary>
        TPageType PageType { get; }
    }

    [ContractClassFor(typeof(IWebPageContentParser))]
    internal abstract class WebPageContentParserContract : IWebPageContentParser
    {
        WebPageContentParsingResult IWebPageContentParser.Parse(WebPage page, WebPageContent content, WebPageContentParsingContext context)
        {
            Contract.Requires<ArgumentNullException>(page != null, "page");
            Contract.Requires<ArgumentNullException>(content != null, "content");
            Contract.Requires<ArgumentNullException>(context != null, "context");
            Contract.Ensures(Contract.Result<WebPageContentParsingResult>() != null);
            return null;
        }

        Task<WebPageContentParsingResult> IWebPageContentParser.ParseAsync(WebPage page, WebPageContent content, WebPageContentParsingContext context,
            CancellationToken cancellationToken)
        {
            Contract.Requires<ArgumentNullException>(page != null, "page");
            Contract.Requires<ArgumentNullException>(content != null, "content");
            Contract.Requires<ArgumentNullException>(context != null, "context");
            Contract.Ensures(Contract.Result<WebPageContentParsingResult>() != null);
            return null;
        }
    }

    [ContractClassFor(typeof(IWebPageContentParser<>))]
    internal abstract class WebPageContentParserContract2<TPageType> : IWebPageContentParser<TPageType>
        where TPageType : struct 
    {
        public WebPageContentParsingResult Parse(WebPage page, WebPageContent content, WebPageContentParsingContext context)
        {
            Contract.Requires<ArgumentException>(Equals(PageType,page.Type), "page.Type");
            return null;
        }

        public Task<WebPageContentParsingResult> ParseAsync(WebPage page, WebPageContent content, WebPageContentParsingContext context,
            CancellationToken cancellationToken)
        {
            Contract.Requires<ArgumentException>(Equals(PageType, page.Type), "page.Type");
            return null;
        }

        public abstract TPageType PageType { get; }
    }
}