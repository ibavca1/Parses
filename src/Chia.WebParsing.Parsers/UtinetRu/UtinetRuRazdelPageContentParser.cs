using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.UtinetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    internal class UtinetRuRazdelPageContentParser : UtinetRuHtmlPageContentParser, IUtinetRuWebPageContentParser
    {
        public UtinetRuWebPageType PageType
        {
            get { return UtinetRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//div[@id='jResultsHeader']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, UtinetRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode razdelsList = content
               .GetSingleNode(@"//ul[@class='b-showcases three-lines']");
            HtmlNodeCollection razdelsNodes = razdelsList
                .GetNodes(@".//a[@class='link']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, UtinetRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}