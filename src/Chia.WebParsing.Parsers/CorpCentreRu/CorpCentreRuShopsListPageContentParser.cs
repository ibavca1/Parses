using System.Collections.Generic;
using Chia.WebParsing.Companies.CorpCentreRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal class CorpCentreRuShopsListPageContentParser : CorpCentreRuHtmlPageContentParser, ICorpCentreRuWebPageContentParser
    {
        public CorpCentreRuWebPageType PageType
        {
            get { return CorpCentreRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//div[@class='shops-list']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//div[@class='shop-item clearfix']");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopNode in shopsNodes)
            {
                string name =
                    shopNode.GetSingleNode(@"h1").GetInnerText();
                string address =
                    shopNode.GetSingleNode(@".//span[@class='address']").GetInnerText();

                var shop = new WebShop(name, address);
                shops.Add(shop);
            }

            return new WebPageContentParsingResult {Shops = shops};
        }
    }
}