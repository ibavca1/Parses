using System.Collections.Generic;
using Chia.WebParsing.Companies.TdPoiskRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal class TdPoiskRuShopsListPageContentParser : TdPoiskRuHtmlPageContentParser, ITdPoiskRuWebPageContentParser
    {
        public TdPoiskRuWebPageType PageType
        {
            get { return TdPoiskRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//ul[@class='b-list m-address']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@"..//li[@class='b-list__item']/*[@class and not(contains(text(),'пункт выдачи'))]");

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