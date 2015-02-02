using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal class KeyRuProductPageContentParser : KeyRuHtmlPageContentParser, IKeyRuWebPageContentParser
    {
        public KeyRuWebPageType Type
        {
            get { return KeyRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@class='catalog_object']");

            string article = productNode
                .GetSingleNode(@".//div[@class='catalog_object_header_options_item']/span[@class='cl_grey_3']/..")
                .GetDigitsText();
            string name = productNode
                .GetSingleNode(".//div[@class='second_line']/h1[@class='header_43']")
                .GetInnerText();
            bool isLegalsOnly =
                productNode.HasNode(".//div[contains(@class,'pre_order_corp_info')]");
            string characteristics = GetCharacteristics(content);
            decimal onlinePrice = 0;
            decimal retailPrice = 0;

            if (!isLegalsOnly)
            {
                onlinePrice = productNode
                    .GetSingleNode(".//div[@class='catalog_object_price_actual']/span")
                    .GetPrice();
                retailPrice = onlinePrice;
            }

            if (characteristics != null && !name.Contains(characteristics))
            {
                name = string.Format("{0} $$ {1}", name, characteristics);
            }

            var product =
                new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri)
                {
                    Characteristics = characteristics
                };

            if (retailPrice != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops =
                    ParseAvailabiltyInShops(content).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static string GetCharacteristics(HtmlPageContent content)
        {
            Dictionary<string, string> characteristics = ParseCharacteristics(content);
            var required = new[]
                               {
                                   "Код производителя", 
                                   "Цвет"
                               };

            string[] values = characteristics
                .Where(c => required.Contains(c.Key))
                .Select(c => c.Value)
                .ToArray();

            return values.Any() ? string.Join(",", values) : null;
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabiltyInShops(HtmlPageContent content)
        {
            HtmlNode shopsListNode =
                content.SelectSingleNode(@"//div[@class='catalog_object_shops_list']");
            bool isNoShops = shopsListNode == null;
            if (isNoShops)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            HtmlNodeCollection shopsNodes = shopsListNode.GetNodes(
                    @".//div[@class='catalog_object_shops_list_item']//span[contains(@class,'catalog_object_shops_list_item_title')]");
            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string address = shopsNode.GetInnerText();
                var shop = new WebProductAvailabilityInShop(address, address, true);
                shops.Add(shop);
            }

            return shops;
        }

        private static Dictionary<string, string> ParseCharacteristics(HtmlPageContent content)
        {
            var characteristics = new Dictionary<string, string>();
            HtmlNodeCollection characteristicsNodes =
                content.SelectNodes(@"//div[@class='catalog_object_characteristics_item']");
            bool noCharacteristics = characteristicsNodes == null;
            if (noCharacteristics)
                return characteristics;

            foreach (HtmlNode characteristicsNode in characteristicsNodes)
            {
                string key =
                    characteristicsNode
                        .SelectSingleNode(@".//div[@class='catalog_object_characteristics_item_attr']")
                        .GetInnerText();
                string value =
                    characteristicsNode
                        .SelectSingleNode(@".//div[@class='catalog_object_characteristics_item_value']")
                        .GetInnerText();
                if (key != null && value != null && !characteristics.ContainsKey(key))
                    characteristics.Add(key, value);
            }

            return characteristics;

        }
    }
}