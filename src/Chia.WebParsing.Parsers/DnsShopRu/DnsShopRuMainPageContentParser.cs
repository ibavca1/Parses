using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.DnsShopRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal class DnsShopRuMainPageContentParser : DnsShopRuHtmlPageContentParser, IDnsShopRuWebPageContentParser
    {
        public DnsShopRuWebPageType Type
        {
            get { return DnsShopRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            HtmlNode catalogNode =
                content.GetSingleNode(@"//li[@id='catalog_nav']/a");
            Uri catalogUri = catalogNode.GetUri(page);
            WebPage catalogPage = page.Site.GetPage(catalogUri, DnsShopRuWebPageType.Razdel, page.Path);
            catalogPage.IsPartOfSiteMap = true;
            pages.Add(catalogPage);

            if (context.Options.InformationAboutShops)
            {
                WebPage shopsPage = page.Site.GetPage(page.Uri, DnsShopRuWebPageType.ShopsList);
                shopsPage.IsPartOfShopsInformation = true;
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}