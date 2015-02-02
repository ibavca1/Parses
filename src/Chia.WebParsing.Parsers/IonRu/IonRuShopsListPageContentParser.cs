using System.Collections.Generic;
using Chia.WebParsing.Companies.IonRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.IonRu
{
    internal class IonRuShopsListPageContentParser : HtmlPageContentParser, IIonRuWebPageContentParser
    {
        public IonRuWebPageType PageType
        {
            get { return IonRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//table[@class='shops_list']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//tbody/tr");

            var shops = new List<WebShop>();
            foreach (HtmlNode shopNode in shopsNodes)
            {
                HtmlNode cityNode = 
                    shopNode.GetSingleNode(@".//td[@class='shop_location']");
                bool isMoscowCity =
                    cityNode.HasNode(@".//strong[@class='shop_metro']");
                string city = isMoscowCity 
                    ? "г. Москва" 
                    : cityNode.GetSingleNode(@"strong").GetInnerText();
                string name = shopNode
                    .GetSingleNode(@".//td[@class='shop_address']/a")
                    .GetInnerText();
                string address = shopNode
                    .GetSingleNode(@".//p[@class='shop_address']")
                    .GetInnerText();
                address = string.Format("{0}, {1}", city, address);

                var shop = new WebShop(name, address);
                shops.Add(shop);
            }

            return new WebPageContentParsingResult {Shops = shops};
        }
    }
}