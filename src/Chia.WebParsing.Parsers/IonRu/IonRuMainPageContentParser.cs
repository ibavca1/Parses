using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.IonRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.IonRu
{
    internal class IonRuMainPageContentParser : HtmlPageContentParser, IIonRuWebPageContentParser
    {
        public IonRuWebPageType PageType
        {
            get { return IonRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode mainMenuNode =
                content.GetSingleNode(@"//ul[@class='menu_bottom']");
            HtmlNodeCollection menuItemsNodes =
                mainMenuNode.GetNodes(@"li/center/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in menuItemsNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, IonRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            if (context.Options.InformationAboutShops)
            {
                Uri uri = content
                    .GetSingleNode(@"//a[@class='b' and text()='Магазины']")
                    .GetUri(page);
                WebPage shopsPage = page.Site.GetPage(uri, IonRuWebPageType.ShopsList, page.Path);
                shopsPage.IsPartOfShopsInformation = true;
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}