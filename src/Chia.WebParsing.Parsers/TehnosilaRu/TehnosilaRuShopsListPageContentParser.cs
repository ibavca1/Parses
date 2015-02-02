using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnosilaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal class TehnosilaRuShopsListPageContentParser : TehnosilaRuHtmlPageContentParser, ITehnosilaRuWebPageContentParser
    {
        public TehnosilaRuWebPageType PageType
        {
            get { return TehnosilaRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsListNode =
                content.GetSingleNode(@"//div[@id='tab-store-list']");
            HtmlNodeCollection shopsNodes =
                shopsListNode.GetNodes(@".//div[contains(@class,'row')]");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name = shopsNode
                    .GetSingleNode(@".//div[@class='name-column']/a")
                    .GetInnerText();
                string address = shopsNode
                    .GetSingleNode(@".//div[@class='address-column']")
                    .GetInnerText();
                var shop = new WebShop(name, address);
                shops.Add(shop);
            }

            return new WebPageContentParsingResult {Shops = shops};
        }
    }
}