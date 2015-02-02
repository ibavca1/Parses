using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.CorpCentreRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal class CorpCentreRuProductPageContentParser : CorpCentreRuHtmlPageContentParser, ICorpCentreRuWebPageContentParser
    {
        public CorpCentreRuWebPageType PageType
        {
            get { return CorpCentreRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[contains(@class,'product-block')]");

            string article =
                productNode.SelectSingleNode(@"//span[@class='code']").GetDigitsText();
            string name =
                productNode.GetSingleNode(@"//div[@class='product-info-in']/h1").GetInnerText();
            decimal onlinePrice = 0;
            decimal retailPrice = 0;

            bool isNotAvailable =
                productNode.HasNode(@".//div[@class='available']//span[@class='noisset']");
            if (!isNotAvailable)
            {
                onlinePrice =
                    content.GetSingleNode(@"//div[@class='price']/div/span").GetPrice();
            }

            bool isOnlineOnly =
                productNode.HasNode(@".//div[@class='available']//span[@class='wait']");
            if (!isOnlineOnly)
            {
                retailPrice = onlinePrice;
            }

            bool isDailyHit =
                productNode.HasNode(@".//div[contains(@class,'goodday-info')]");
            if (isDailyHit)
            {
                retailPrice = onlinePrice;
                onlinePrice = 0;
            }

            var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri);
            if (onlinePrice != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = ParseAvailabilityInShops(content).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityInShops(HtmlPageContent content)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//div[@id='popup-amount']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//tbody/tr");

            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name =
                    shopsNode.GetSingleNode(@".//td[@class='shop-name']/a").GetInnerText();
                bool isAvailable =
                    shopsNode.HasNode(@".//span[@class='limited']");

                var shop = new WebProductAvailabilityInShop(name, isAvailable);
                shops.Add(shop);
            }

            return shops;
        }
    }
}