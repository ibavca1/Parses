using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CitilinkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    internal class CitilinkRuRazdelPageContentParser : CitilinkRuHtmlPageContentParser, ICitilinkRuWebPageContentParser
    {
        public CitilinkRuWebPageType PageType
        {
            get { return CitilinkRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage =
                content.HasNode(@"//div[@class='cat_nav top']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, CitilinkRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNodeCollection razdelsNodes =
                content.GetNodes(@"//div[contains(@class,'cat-item')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetAttributeText("title");
                if (string.IsNullOrWhiteSpace(name))
                    continue;

                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, CitilinkRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}