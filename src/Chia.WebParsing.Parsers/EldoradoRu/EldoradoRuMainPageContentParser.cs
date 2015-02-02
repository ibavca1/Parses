using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EldoradoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal class EldoradoRuMainPageContentParser : EldoradoRuHtmlPageContentParser, IEldoradoRuWebPageContentParser
    {
        public EldoradoRuWebPageType Type
        {
            get { return EldoradoRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNodeCollection menuItemsNodes =
                content.SelectNodes("//a[@class='headerCatalogItemLink' and not(contains(@class,'Promo'))]");
            menuItemsNodes.Remove(0);

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                string name = menuItemNode.GetInnerText();
                Uri uri = menuItemNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);

                var catalogPage = page.Site.GetPage(uri, EldoradoRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            if (context.Options.InformationAboutShops)
            {
                HtmlNode shopsNode =
                    content.GetSingleNode(@"//a[@title='Адреса магазинов']");
                Uri uri = shopsNode.GetUri(page);
                WebPage shopListPage = page.Site.GetPage(uri, EldoradoRuWebPageType.ShopsList);
                shopListPage.IsPartOfShopsInformation = true;
                pages.Add(shopListPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}