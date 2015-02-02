using System.Collections.Generic;
using Chia.WebParsing.Companies.KeyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal class KeyRuShopsListPageContentParser : KeyRuHtmlPageContentParser, IKeyRuWebPageContentParser
    {
        public KeyRuWebPageType Type
        {
            get { return KeyRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNodeCollection shopsNodes =
                content.GetNodes(@"//div[@class='shops_list_item']");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string address = shopsNode
                    .GetSingleNode(@".//div[@class='title_line']/a/span")
                    .GetInnerText();
                var shop = new WebShop(address, address);
                shops.Add(shop);
            }

            return new WebPageContentParsingResult {Shops = shops};

        }
    }
}