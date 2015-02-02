using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal class TdPoiskRuMainPageContentParser : TdPoiskRuHtmlPageContentParser, ITdPoiskRuWebPageContentParser
    {
        public TdPoiskRuWebPageType PageType
        {
            get { return TdPoiskRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            WebPage[] menuPages = ParseMenuPages(page, content).ToArray();
            pages.AddRange(menuPages);

            if (context.Options.InformationAboutShops)
            {
                WebPage shopsPage = ParseShopsPage(page, content);
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseMenuPages(WebPage page, HtmlPageContent content)
        {
            HtmlNode menuNode =
               content.GetSingleNode(@"//div[@id='main_navigation']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//a[@class='b-link b-level0__link']");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemsNode in menuItemsNodes)
            {
                string name = menuItemsNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = menuItemsNode.GetUri(page);
                WebPage menuItemsPage = page.Site.GetPage(uri, TdPoiskRuWebPageType.Razdel, path);
                menuItemsPage.IsPartOfSiteMap = true;
                pages.Add(menuItemsPage);
            }

            return pages;
        }

        private static WebPage ParseShopsPage(WebPage page, HtmlPageContent content)
        {
            Uri shopsPageUri =
                content.GetSingleNode(@"//a[contains(text(),'Адреса магазинов')]").GetUri(page);
            WebPage shopsPage = page.Site.GetPage(shopsPageUri, TdPoiskRuWebPageType.ShopsList, page.Path);
            shopsPage.IsPartOfShopsInformation = true;
            return shopsPage;
        }
    }
}