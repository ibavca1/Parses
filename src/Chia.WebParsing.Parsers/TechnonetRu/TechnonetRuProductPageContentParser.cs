using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal class TechnonetRuProductPageContentParser : TechnonetRuHtmlPageContentParser, ITechnonetRuWebPageContentParser
    {
        public TechnonetRuWebPageType PageType
        {
            get { return TechnonetRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string description =
                content.GetSingleNode(@"//div[@class='b-catalog-descr']/b").GetInnerText();
            string name = 
                content.GetSingleNode(@"//div[@class='b-catalog-descr']/h1").GetInnerText();
            decimal price =
                content.GetSingleNode(@"//span[contains(@class,'b-catalog-prow3')]").GetPrice();

            var product = new WebMonitoringPosition(null, name, price, page.Uri)
            {
                Characteristics = description
            };

            if (price != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = ParseAvailabilityInShops(content).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityInShops(HtmlPageContent content)
        {
            HtmlNodeCollection shopsNodes =
                content.SelectNodes(@"//div[contains(@class,'b-catalog-obuy')]/p/a/..");
            bool noShops = shopsNodes == null;
            if (noShops)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name = shopsNode.GetSingleNode(@".//a[@rel='shop']").GetInnerText();
                bool isAvailable = shopsNode.DoesNotHaveNode(@".//span[text()='нет в наличии']");

                var shop = new WebProductAvailabilityInShop(name, isAvailable);
                shops.Add(shop);
            }

            return shops.Distinct();
        }
    }
}