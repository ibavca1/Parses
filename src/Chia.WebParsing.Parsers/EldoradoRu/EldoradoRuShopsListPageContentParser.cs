using System.Collections.Generic;
using Chia.WebParsing.Companies.EldoradoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal class EldoradoRuShopsListPageContentParser : EldoradoRuHtmlPageContentParser, IEldoradoRuWebPageContentParser
    {
        public EldoradoRuWebPageType Type
        {
            get { return EldoradoRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//div[@class='shops_list']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//div[@class='shop-item' and @data-type='Розничный магазин']");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name =
                    shopsNode.GetSingleNode(@".//div[@class='name']/a").GetInnerText();
                string address =
                    shopsNode.GetSingleNode(@".//div[@class='metro']/p[not(@style)]").GetInnerText();
                var shop = new WebShop(name, address);
                shops.Add(shop);
            }

            return new WebPageContentParsingResult {Shops = shops};
        }
    }
}