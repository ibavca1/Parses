using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.DnsShopRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal class DnsShopRuShopsListPageContentParser : DnsShopRuHtmlPageContentParser, IDnsShopRuWebPageContentParser
    {
        public DnsShopRuWebPageType Type
        {
            get { return DnsShopRuWebPageType.ShopsList; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode shopsList =
                content.GetSingleNode(@"//ul[@class='addr']");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//dd/a[not(text()='Интернет-магазин')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                Uri uri = shopsNode.GetUri(page);
                WebPage shopPage = page.Site.GetPage(uri, DnsShopRuWebPageType.Shop);
                shopPage.IsPartOfShopsInformation = true;
                pages.Add(shopPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}