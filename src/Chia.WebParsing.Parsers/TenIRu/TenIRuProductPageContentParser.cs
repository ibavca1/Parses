using Chia.WebParsing.Companies.TenIRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TenIRu
{
    internal class TenIRuProductPageContentParser : TenIRuHtmlPageContentParser, ITenIRuWebPageContentParser
    {
        public TenIRuWebPageType PageType
        {
            get { return TenIRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode = content
                .GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");

            string article = productNode
                .GetSingleNode(@".//div[@class='article']")
                .GetDigitsText();
            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            decimal price = productNode
                .GetSingleNode(@".//span[@class='js-start-price']")
                .GetPrice();

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}