using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.OldiRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OldiRu
{
    internal class OldiRuRazdelPageContentParser : OldiRuHtmlPageContentParser, IOldiRuWebPageContentParser
    {
        public OldiRuWebPageType PageType
        {
            get { return OldiRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//form[@id='filterform']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, OldiRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode categoriesList =
                content.GetSingleNode(@"//div[@id='itemscontainer']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//div[@class='smallparamscont']/h3[@class='cmtitle']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = categoryNode.GetUri(page);
                var categoryPage = page.Site.GetPage(uri, OldiRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}