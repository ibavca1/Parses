using Chia.WebParsing.Companies.NotikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NotikRu
{
    internal class NotikRuProductPageContentParser : NotikRuHtmlPageContentParser, INotikRuWebPageContentParser
    {
        public NotikRuWebPageType PageType
        {
            get { return NotikRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string article = content
                .SelectSingleNode(@"//strong[text()='Артикул:']/../following-sibling::td")
                .GetDigitsText();
            string name = content
                .GetSingleNode(@"//div[@class='content inset']/h2")
                .GetInnerText();
            decimal price = content
                .GetSingleNode(@"//div[@class='priceDiv']")
                .GetPrice();

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}