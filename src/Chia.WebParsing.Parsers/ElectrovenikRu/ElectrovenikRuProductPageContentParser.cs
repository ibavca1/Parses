using Chia.WebParsing.Companies.ElectrovenikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    internal class ElectrovenikRuProductPageContentParser : ElectrovenikRuHtmlPageContentParser, IElectrovenikRuWebPageContentParser
    {
        public ElectrovenikRuWebPageType PageType
        {
            get { return ElectrovenikRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");

            string article = productNode
                .GetSingleNode(@".//div[@itemprop='productID']")
                .GetInnerText()
                .Replace("Артикул:", "").Trim();
            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            decimal cost = productNode
                .GetSingleNode(@".//div[@itemprop='price']/strong/text()")
                .GetPrice();

            var product = new WebMonitoringPosition(article, name, cost, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);

        }
    }
}