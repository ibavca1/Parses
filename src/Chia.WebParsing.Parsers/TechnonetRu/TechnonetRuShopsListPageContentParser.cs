using System.Collections.Generic;
using Chia.WebParsing.Companies.TechnonetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal class TechnonetRuShopsListPageContentParser : TechnonetRuHtmlPageContentParser, ITechnonetRuWebPageContentParser
    {
        public TechnonetRuWebPageType PageType
        {
            get { return TechnonetRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//div[@class='b-shopprev']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//a[@class='cboxElement' and @rel='shop']");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name = shopsNode.GetInnerText();
                var shop = new WebShop(name);
                shops.Add(shop);
            }

            return new WebPageContentParsingResult {Shops = shops};
        }
    }
}