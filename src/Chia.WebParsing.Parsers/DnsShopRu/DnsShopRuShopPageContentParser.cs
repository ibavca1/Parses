using Chia.WebParsing.Companies.DnsShopRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal class DnsShopRuShopPageContentParser : DnsShopRuHtmlPageContentParser, IDnsShopRuWebPageContentParser
    {
        public DnsShopRuWebPageType Type
        {
            get { return DnsShopRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = content
                .GetSingleNode(@"//div[@class='container']/h1")
                .GetInnerText();
            string address = content
                .GetSingleNode(@"//span[@class='street-address']")
                .GetInnerText();

            var shop = new WebShop(name, address);
            return WebPageContentParsingResult.FromShop(shop);
        }
    }
}