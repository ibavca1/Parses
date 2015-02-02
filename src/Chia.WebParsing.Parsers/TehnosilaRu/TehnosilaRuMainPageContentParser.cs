using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal class TehnosilaRuMainPageContentParser : TehnosilaRuHtmlPageContentParser, ITehnosilaRuWebPageContentParser
    {
        public TehnosilaRuWebPageType PageType
        {
            get { return TehnosilaRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            WebPage[] mainMenuPages = ParseMainMenu(page, content).ToArray();
            pages.AddRange(mainMenuPages);

            if (context.Options.InformationAboutShops)
            {
                WebPage shopsPage = ParseShopsListPage(page, content);
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseMainMenu(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection menuItemsNodes =
                content.GetNodes(@"//a[contains(@class, 'has-children')]");
            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                string name = menuItemNode.GetInnerText();
                Uri uri = menuItemNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);

                WebPage listPage = page.Site.GetPage(uri, TehnosilaRuWebPageType.Razdel, path);
                listPage.IsPartOfSiteMap = true;
                pages.Add(listPage);
            }

            return pages;
        }

        private static WebPage ParseShopsListPage(WebPage page, HtmlPageContent content)
        {
            Uri shopsPageUri = content
                .GetSingleNode(@"//div[@id='header-links']/a[text()='Магазины']")
                .GetUri(page);

            WebPage shopsPage = page.Site.GetPage(shopsPageUri, TehnosilaRuWebPageType.ShopsList, page.Path);
            shopsPage.IsPartOfShopsInformation = true;
            return shopsPage;
        }
    }
}