using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnoshokRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal class TehnoshokRuShopsListPageContentParser : TehnoshokRuHtmlPageContentParser, ITehnoshokRuWebPageContentParser
    {
        public TehnoshokRuWebPageType PageType
        {
            get { return TehnoshokRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//section[contains(@class,'box shop-addr')]");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//div[@data-address]");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopNode in shopsNodes)
            {
                string address =
                    shopNode.GetSingleNode(@".//span[@class='addr']").GetInnerText();
                var shop = new WebShop(address);
                shops.Add(shop);
            }
            
            return new WebPageContentParsingResult {Shops = shops};
        }
    }
}