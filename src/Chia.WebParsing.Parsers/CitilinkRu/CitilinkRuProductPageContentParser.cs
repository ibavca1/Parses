using System;
using System.Linq;
using Chia.WebParsing.Companies.CitilinkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    internal class CitilinkRuProductPageContentParser : CitilinkRuHtmlPageContentParser, ICitilinkRuWebPageContentParser
    {
        public CitilinkRuWebPageType PageType
        {
            get { return CitilinkRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string article = 
                content.GetSingleNode(@"//div[@id='item_new']//span").GetDigitsText();
            string name = 
            content.GetSingleNode(@"//div[@id='item_new']//h1").GetInnerText();
            decimal price = 0;

            bool isAvailable =
                content.DoesNotHaveNode(@"//div[@class='price not-exist']");
            if (isAvailable)
            {
                price = 
                    content.GetSingleNode(@".//div[@class='price']").GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            //if (context.Options.ShopsAvailability)
            //{
            //    string[] shops = ParseShopsAvailability(document).ToArray();
            //    product.ShopsPrices =
            //        shops.Select(shop => new WebShopPrice(shop, price, uri)).ToArray();
            //}

            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}