using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TehnoshokRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal class TehnoshokRuProductPageContentParser : TehnoshokRuHtmlPageContentParser, ITehnoshokRuWebPageContentParser
    {
        public TehnoshokRuWebPageType PageType
        {
            get { return TehnoshokRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@class='card']");
            bool isError = IsError(productNode);
            if (isError)
                return WebPageContentParsingResult.Empty;
            string article =
                productNode
                    .GetSingleNode(@".//p[@class='code']").GetInnerText()
                    .Replace("Код товара:", "").Trim();
            string name =
                productNode.GetSingleNode(@".//div[@class='main-title']/h1").GetInnerText();

            bool isOutOfStock =
                productNode.HasNode(".//span[@class='not-available-notify']");
            decimal price = 0;

            if (!isOutOfStock)
            {
                price =
                    productNode.GetSingleNode(@".//span[@class='cell new']").GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);

            if (price != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops =
                    ParseAvailabilityInShops(content.Document).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static bool IsError(HtmlNode productNode)
        {
            return productNode.HasNode(@".//div[@class='alert alert-error']");
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityInShops(HtmlDocument document)
        {
            HtmlNode shopsList =
                document.DocumentNode.GetSingleNode(@"//div[@class='shops']");

            bool isType1 = shopsList.HasNode(@".//h2[contains(text(),'Забрать товар')]");
            if (isType1)
                return ParseShopsListType1(shopsList);

            bool isType2 = shopsList.HasNode(@".//h2[contains(text(),'Забрать сейчас')]");
            if (isType2)
                return ParseShopsListType2(shopsList);

            throw new InvalidWebPageMarkupException();
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseShopsListType1(HtmlNode shopsList)
        {
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//tbody/tr");
            bool isAvailable = true;
            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopNode in shopsNodes)
            {
                HtmlNode node =
                    shopNode.SelectSingleNode(@".//td[@colspan]");
                if (node != null)
                {
                    isAvailable = node.GetInnerText().Contains(@"сегодня");
                    continue;
                }

                string name =
                    shopNode
                        .GetSingleNode(@".//td[@style='']")
                        .GetInnerText();
                var shop = new WebProductAvailabilityInShop(name, isAvailable);
                shops.Add(shop);
            }

            return shops;
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseShopsListType2(HtmlNode shopsList)
        {
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//tbody/tr");
            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopNode in shopsNodes)
            {
                string name =
                    shopNode.GetSingleNode(@".//td[2]").GetInnerText();
                var shop = new WebProductAvailabilityInShop(name, true);
                shops.Add(shop);
            }

            return shops;
        }
    }
}