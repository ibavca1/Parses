using Chia.WebParsing.Companies.OldiRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OldiRu
{
    internal class OldiRuProductPageContentParser : OldiRuHtmlPageContentParser, IOldiRuWebPageContentParser
    {
        public OldiRuWebPageType PageType
        {
            get { return OldiRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//*[@itemtype = 'http://schema.org/Product']");

            string article = productNode
                .GetSingleNode(@".//div[contains(@class,'cte_w_articul_sep')]")
                .GetInnerText();
            string name = productNode
                .GetSingleNode(@".//span[@itemprop='name']/text()")
                .GetInnerText();
            string characteristics = productNode
                .SelectSingleNode(".//div[contains(@class,'itemnamedesrc')]")
                .GetInnerText();
            decimal onlinePrice = 0;
            decimal retailPrice = 0;

            //bool isAbsent = productNode.HasNode(@"//span[text()='нет в наличии']");
            //if (!isAbsent)
            {
                onlinePrice = productNode
                    .GetSingleNode(@".//span[@itemprop='price']")
                    .GetPrice();
                retailPrice = productNode
                    .SelectSingleNode(@".//div[@class='cte_w_shop_price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri) {Characteristics = characteristics};
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}