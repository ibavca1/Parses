using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TechnonetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal class TechnonetRuMainPageContentParser : TechnonetRuHtmlPageContentParser, ITechnonetRuWebPageContentParser
    {
        public TechnonetRuWebPageType PageType
        {
            get { return TechnonetRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            WebPage[] menuPages = ParseMainMenu(page, content).ToArray();
            pages.AddRange(menuPages);

            if (context.Options.InformationAboutShops)
            {
                WebPage shopsPage = ParseShopsPage(page, content);
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseMainMenu(WebPage page, HtmlPageContent content)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//menu[@id='b-rootmenu']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.SelectNodes(@".//a[@class='b-rootmenu-a']");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemsNode in menuItemsNodes)
            {
                string name = menuItemsNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = menuItemsNode.GetUri(page);
                WebPage menuItemsPage = page.Site.GetPage(uri, TechnonetRuWebPageType.Razdel, path);
                menuItemsPage.IsPartOfSiteMap = true;
                pages.Add(menuItemsPage);
            }

            return pages;
        }

        private static WebPage ParseShopsPage(WebPage page, HtmlPageContent content)
        {
            Uri uri = content.GetSingleNode(@"//p[@class='b-shops']/a").GetUri(page);
            WebPage shopsPage = page.Site.GetPage(uri, TechnonetRuWebPageType.ShopsList, page.Path);
            shopsPage.IsPartOfShopsInformation = true;
            return shopsPage;
        }
    }
}