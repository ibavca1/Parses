using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace WebParsingFramework
{
    /// <summary>
    /// ������ ����������� ���-��������.
    /// </summary>
    [ContractClass(typeof (WebPageContentParserContract))]
    public interface IWebPageContentParser
    {
        /// <summary>
        /// ���������� ������ �������� ���-��������.
        /// </summary>
        /// <param name="page">��������� ��������, ������ ������� ������������.</param>
        /// <param name="content">������� ��������, ������ �������� ������������.</param>
        /// <param name="context">��������, � ������ �������� ������������ ������.</param>
        /// <returns>��������� �������.</returns>
        WebPageContentParsingResult Parse(WebPage page, WebPageContent content, WebPageContentParsingContext context);

        /// <summary>
        /// ���������� ����������� ������ �������� ���-��������.
        /// </summary>
        /// <param name="page">��������� ��������, ������ ������� ������������.</param>
        /// <param name="content">������� ��������, ������ �������� ������������.</param>
        /// <param name="context">��������, � ������ �������� ������������ ������.</param>
        /// <param name="cancellationToken">����� <see cref="CancellationToken"/>, ������� ����� �������� ������.</param>
        /// <returns>������, ���������� ��������� �������.</returns>
        Task<WebPageContentParsingResult> ParseAsync(WebPage page, WebPageContent content,
            WebPageContentParsingContext context, CancellationToken cancellationToken);
    }

    /// <summary>
    /// ������ ����������� ���-��������.
    /// </summary>
    [ContractClass(typeof(WebPageContentParserContract2<>))]
    public interface IWebPageContentParser<out TPageType> : IWebPageContentParser
        where TPageType : struct 
    {
        /// <summary>
        /// ��� ���-��������.
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