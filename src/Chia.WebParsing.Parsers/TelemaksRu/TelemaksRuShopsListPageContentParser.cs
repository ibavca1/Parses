using System.Collections.Generic;
using Chia.WebParsing.Companies.TelemaksRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal class TelemaksRuShopsListPageContentParser : TelemaksRuHtmlPageContentParser, ITelemaksRuWebPageContentParser
    {
        public TelemaksRuWebPageType PageType
        {
            get { return TelemaksRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//ul[@class='spisok_shops']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@"li/span[not(contains(text(),'Уцененные товары'))]");

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