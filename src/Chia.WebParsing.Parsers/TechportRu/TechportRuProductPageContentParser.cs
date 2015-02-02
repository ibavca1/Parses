using Chia.WebParsing.Companies.TechportRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechportRu
{
    internal class TechportRuProductPageContentParser : TechportRuHtmlPageContentParser, ITechportRuWebPageContentParser
    {
        public TechportRuWebPageType PageType
        {
            get { return TechportRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            TechportRuCity city = TechportRuCity.Get(page.Site.City);
            HtmlNode productNode =
                content.GetSingleNode(@"//*[@itemtype='http://schema.org/Product']");

            string article = productNode
                .GetSingleNode(@".//span[@itemprop='sku']")
                .GetInnerText();
            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            decimal price = GetPrice(city, productNode);
            
            var position = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(position);
        }
    }
}