using Chia.WebParsing.Companies.TehnoparkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    internal class TehnoparkRuProductPageContentParser : TehnoparkRuHtmlPageContentParser, ITehnoparkRuWebPageContentParser
    {
        public TehnoparkRuWebPageType PageType
        {
            get { return TehnoparkRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode = content
                .GetSingleNode(@".//div[@itemtype='http://schema.org/Product']");
            string article = productNode
                .GetSingleNode(@".//*[@itemprop='productID']")
                .GetInnerText();
            string name = productNode
                .GetSingleNode(@".//*[@itemprop='name']")
                .GetInnerText();
            decimal price = 0;

            bool isOutOfStock = productNode
                .HasNode(".//li[contains(text(),'Наличие: нет в наличии')]");
            if (!isOutOfStock)
            {
                price = productNode
                    .GetSingleNode(@"//*[@itemprop='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}