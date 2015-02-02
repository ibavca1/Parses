using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal class DnsShopRuProductPageContentParser : DnsShopRuHtmlPageContentParser, IDnsShopRuWebPageContentParser
    {
        public DnsShopRuWebPageType Type
        {
            get { return DnsShopRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNode productNode = content
                .GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");
            string article = productNode
                .GetSingleNode(@".//li[@class='code']/span")
                .GetDigitsText();
            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            string characteristics = productNode
                .SelectSingleNode(@".//div[@class='price_specs']")
                .GetInnerText();
            bool isAvailable = productNode
                .HasNode(@".//a[@class='price_btn button cart add']");
            decimal price = 0;
            if (isAvailable)
            {
                price = productNode
                    .GetSingleNode(@".//div[@class='item_new' or @class='item_norm']/span[@class='item_value']")
                    .GetPrice();
            }

            if (!string.IsNullOrWhiteSpace(characteristics))
            {
                name = string.Format("{0} $$ {1}", name, characteristics);
            }

            var product =
                new WebMonitoringPosition(article, name, price, price, page.Uri)
                    {
                        Characteristics = characteristics
                    };

            if (context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = ParseAvailabilityInShops(content).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityInShops(HtmlPageContent document)
        {
            bool noShops =
                document.HasNode(@"//div[text()='“овара нет в наличии в магазинах вашего города']");
            if (noShops)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            HtmlNodeCollection shopsNodes =
                document.GetNodes(@"//li[@class='shops']//li/a/text()");
            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name = shopsNode.GetInnerText();
                var shop = new WebProductAvailabilityInShop(name, true);
                shops.Add(shop);
            }

            return shops;
        }
    }
}