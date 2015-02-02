using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.UlmartRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal class UlmartRuShopsListPageContentParser : HtmlPageContentParser, IUlmartRuWebPageContentParser
    {
        public UlmartRuWebPageType PageType
        {
            get { return UlmartRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool hasCentralShops =
                content.HasNode(@"//h3[contains(text(),'Центральные магазины')]");
            return
                hasCentralShops
                    ? ParseCentralShops(page, content)
                    : ParseShops(content);
        }

        private static WebPageContentParsingResult ParseCentralShops(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection shopsNodes =
                content.GetNodes(@".//h3[contains(text(),'Центральные магазины')]/../p[1]/a[not(@class)]");

            var pages = new List<WebPage>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                Uri uri = shopsNode.GetUri(page);
                WebPage shopPage = page.Site.GetPage(uri, UlmartRuWebPageType.Shop, page.Path);
                shopPage.IsPartOfShopsInformation = true;
                pages.Add(shopPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPageContentParsingResult ParseShops(HtmlPageContent content)
        {
            HtmlNode shopsList =
                    content.GetSingleNode(@"//h1[text()='Адреса и контакты']/..");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@"div/p/strong");

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