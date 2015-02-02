using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal class UlmartRuProductPageContentParser : HtmlPageContentParser, IUlmartRuWebPageContentParser
    {
        public UlmartRuWebPageType PageType
        {
            get { return UlmartRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//section[@itemtype='http://schema.org/Product']");

            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            string article = productNode
                .GetSingleNode(@".//span[@class='b-art__num']")
                .GetInnerText();
            string characteristics = productNode
                .GetSingleNode(@".//div[@data-reload-id='subcaption']")
                .GetInnerText();
            decimal price = 0;

            bool isAvailable =
                productNode.HasNode(".//div[contains(text(),'Есть в наличии')]");
            if (isAvailable)
            {
                price = productNode
                    .GetSingleNode(@".//span[@itemprop='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, price, page.Uri)
            {
                Characteristics = characteristics
            };

            if (price != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = ParseShopsAvailability(content).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseShopsAvailability(HtmlPageContent content)
        {
            int headsCount = 0;
            HtmlNodeCollection shopsNodes =
               content.GetNodes(@"//div[contains(@class,'b-table__body')]/div");
            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string clss = shopsNode.GetAttributeText("class");
                if (clss.Contains("b-table__row_head"))
                {
                    if (++headsCount == 2)
                        break;
                    continue;
                }

                string name = shopsNode
                    .GetSingleNode(@".//div[@title]")
                    .GetAttributeText(@"title")
                    .Replace("\r\n", "").Trim();
                bool isAvailable = 
                    shopsNode.HasNode(@".//span[@class='text-success']");

                var shop = new WebProductAvailabilityInShop(name, isAvailable);
                shops.Add(shop);
            }

            return shops.Distinct();
        }
    }
}