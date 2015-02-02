using Chia.WebParsing.Companies.PultRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.PultRu
{
    internal class PultRuProductPageContentParser : PultRuHtmlPageContentParser, IPultRuWebPageContentParser
    {
        public PultRuWebPageType PageType
        {
            get { return PultRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool is404Error = content.HasNode(@"//h3[contains(text(),'Ошибка 404 (Not Found)')]");
            if (is404Error)
                return WebPageContentParsingResult.Empty;

            HtmlNode productNode =
                content.GetSingleNode("//div[@itemtype='http://data-vocabulary.org/Product']");

            string name = productNode
                .GetSingleNode(".//h1")
                .GetInnerText();
            string article = productNode
                .SelectSingleNode(".//div[@itemprop='identifier']//b")
                .GetInnerText();
            decimal price = 0;

            bool isAvailable = 
                productNode.HasNode(".//div[@class='availability yes']") &&
                productNode.DoesNotHaveNode(".//span[@class='bigCallBtn']");
            bool hasPrice =
                productNode.HasNode(@".//div[@itemtype='http://data-vocabulary.org/Offer']");
            if (isAvailable && hasPrice)
            {
                price = 
                    productNode.GetSingleNode(".//span[@itemprop='average']").GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}