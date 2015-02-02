using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TelemaksRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal class TelemaksRuMainPageContentParser : TelemaksRuHtmlPageContentParser, ITelemaksRuWebPageContentParser
    {
        public TelemaksRuWebPageType PageType
        {
            get { return TelemaksRuWebPageType.Main; }
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
            HtmlNode mainMenu =
                content.GetSingleNode(@"//div[@class='MenuBigContainer']");
            HtmlNodeCollection menuItemsNodes =
                 mainMenu.GetNodes("//a[contains(@class,'menu_link_up')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                string name = menuItemNode.GetInnerText();
                Uri uri = menuItemNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);

                WebPage catalogPage = page.Site.GetPage(uri, TelemaksRuWebPageType.Razdel, path);
                //WebPage catalogPage = page.Site.GetPage(uri, TelemaksRuWebPageType.ProductsList, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return pages;
        }

        private static WebPage ParseShopsPage(WebPage page, HtmlPageContent content)
        {
            Uri shopsUri =
                content.SelectSingleNode(@"//a[@class='address-shop-header']").GetUri(page);

            WebPage shopsPage = page.Site.GetPage(shopsUri, TelemaksRuWebPageType.ShopsList, page.Path);
            shopsPage.IsPartOfShopsInformation = true;
            return shopsPage;
        }
    }
}