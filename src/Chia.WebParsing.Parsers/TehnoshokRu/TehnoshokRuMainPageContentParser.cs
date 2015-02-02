using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnoshokRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal class TehnoshokRuMainPageContentParser : TehnoshokRuHtmlPageContentParser, ITehnoshokRuWebPageContentParser
    {
        public TehnoshokRuWebPageType PageType
        {
            get { return TehnoshokRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuContainer =
                content.GetSingleNode(@"//div[@class='amenu']");
            HtmlNodeCollection menuItemsNodes =
                menuContainer.GetNodes(@".//a[@class='first-items-link']");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                HtmlNode nameNode =
                    menuItemNode.GetSingleNode(@"span[@class='item-title']");

                Uri uri = menuItemNode.GetUri(page);
                string name = nameNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, TehnoshokRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            if (context.Options.InformationAboutShops)
            {
                Uri shopsPageUri = content
                    .GetSingleNode(@"//span[text()='Адреса магазинов']")
                    .ParentNode
                    .GetUri(page);
                WebPage shopsPage = page.Site.GetPage(shopsPageUri, TehnoshokRuWebPageType.ShopsList, page.Path);
                shopsPage.IsPartOfShopsInformation = true;
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}