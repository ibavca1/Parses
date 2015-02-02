using System;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers
{
    [ContractClass(typeof (HtmlPageContentParserContract))]
    public abstract class HtmlPageContentParser : IWebPageContentParser 
    {
        public virtual WebPageContentParsingResult Parse(WebPage page, WebPageContent content, WebPageContentParsingContext context)
        {
            HtmlPageContent html = HtmlPageContent.Create(content);
            ValidateContent(page, html, context);
            return ParseHtml(page, html, context);
        }

        public virtual Task<WebPageContentParsingResult> ParseAsync(WebPage page, WebPageContent content, WebPageContentParsingContext context,
            CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() => Parse(page, content, context), cancellationToken);
        }

        public abstract WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context);

        protected virtual void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            Contract.Requires<ArgumentNullException>(page != null, "page");
            Contract.Requires<ArgumentNullException>(content != null, "content");
            Contract.Requires<ArgumentNullException>(context != null, "context");
        }
    }

    [ContractClassFor(typeof (HtmlPageContentParser))]
    abstract class HtmlPageContentParserContract : HtmlPageContentParser
    {
        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            Contract.Requires<ArgumentNullException>(page != null, "page");
            Contract.Requires<ArgumentNullException>(content != null, "content");
            Contract.Requires<ArgumentNullException>(context != null, "context");
            Contract.Ensures(Contract.Result<WebPageContentParsingResult>() != null);

            return null;
        }
    }
}
