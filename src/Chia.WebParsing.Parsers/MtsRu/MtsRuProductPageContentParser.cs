using Chia.WebParsing.Companies.MtsRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MtsRu
{
    internal class MtsRuProductPageContentParser : HtmlPageContentParser, IMtsRuWebPageContentParser
    {
        public MtsRuWebPageType PageType
        {
            get { return MtsRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@id='right_block']");

            string name = productNode
                .SelectSingleNode(@".//ul[@class='nav_pachwey pachwey']/li[last()]")
                .GetInnerText();
            decimal price = 0;
            name = RemoveTownFromName(name);

            bool outOfStock = productNode.HasNode(@".//span[contains(@class,'outofstock')]");
            if (!outOfStock)
            {
                price = productNode
                    .GetSingleNode(@".//span[@itemprop='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(null, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static string RemoveTownFromName(string name)
        {
            int townIndex = name.LastIndexOf(" в ");
            return townIndex != -1 ? name.Remove(name.LastIndexOf(" в ")) : name;
        }
    }
}