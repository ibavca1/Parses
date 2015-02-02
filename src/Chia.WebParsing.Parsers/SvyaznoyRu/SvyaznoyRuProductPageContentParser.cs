using Chia.WebParsing.Companies.SvyaznoyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    internal class SvyaznoyRuProductPageContentParser : SvyaznoyRuHtmlPageContentParser, ISvyaznoyRuWebPageContentParser
    {
        public SvyaznoyRuWebPageType PageType
        {
            get { return SvyaznoyRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://data-vocabulary.org/Product']");

            string name = 
                productNode.GetSingleNode(@".//div[@class='tovar-tl']/h1").GetInnerText();
            string article =
                productNode.GetSingleNode(@".//div[@class='tovar-tl']/span[@class='tovar-id']").GetDigitsText();
            decimal price = 0;

            bool isAbsent =
                productNode.HasNode(@".//div[@class='no-tovar-block']");
            if (!isAbsent)
            {
                price = productNode.GetSingleNode(@".//span[@itemprop='price']").GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}