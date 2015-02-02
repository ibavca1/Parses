using Chia.WebParsing.Companies.RbtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RbtRu
{
    internal class RbtRuProductPageContentParser : RbtRuHtmlPageContentParser, IRbtRuWebPageContentParser
    {
        public RbtRuWebPageType PageType
        {
            get { return RbtRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = 
                content.GetSingleNode(@"//h1[@itemprop='name']").GetInnerText();
            decimal price = 
                content.GetSingleNode(@"//div[@class='price']/span").GetPrice();

            var product = new WebMonitoringPosition(null, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}