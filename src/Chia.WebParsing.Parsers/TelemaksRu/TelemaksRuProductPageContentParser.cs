using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal class TelemaksRuProductPageContentParser : TelemaksRuHtmlPageContentParser, ITelemaksRuWebPageContentParser
    {
        public TelemaksRuWebPageType PageType
        {
            get { return TelemaksRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@class='ProductCarts']");

            string name = 
                productNode.GetSingleNode(@"//h1[contains(@class,'title')]").GetInnerText();
            string article =
                productNode.GetSingleNode(@"//div[@class='ProductCartСode']/p").GetDigitsText();
            decimal onlinePrice = 0;
            decimal retailPrice =
                productNode.GetSingleNode(@"//p[@class='OneProductPrice_rz']/span").GetPrice();

            bool hasOnlinePrice =
                productNode.HasNode(@".//p[text()='Цена интернет-магазина:']");
            if (hasOnlinePrice)
            {
                onlinePrice = productNode.GetSingleNode(@"//p[@class='priceim']").GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri);

            if (retailPrice != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = ParseAvailabilityShops(content).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityShops(HtmlPageContent content)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//div[@id='goods_shops']");
            HtmlNodeCollection shopsNodes =
                shopsList.SelectNodes(@".//td[contains(@class,'first')]/a");
            bool noShops = shopsNodes == null;
            if (noShops)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode nameNode in shopsNodes)
            {
                string name = nameNode.GetInnerText();
                var shop = new WebProductAvailabilityInShop(name, true);
                shops.Add(shop);
            }

            return shops;
        }
    }
}