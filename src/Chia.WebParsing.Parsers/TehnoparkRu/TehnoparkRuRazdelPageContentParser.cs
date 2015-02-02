using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnoparkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    internal class TehnoparkRuRazdelPageContentParser : TehnoparkRuHtmlPageContentParser, ITehnoparkRuWebPageContentParser
    {
        public TehnoparkRuWebPageType PageType
        {
            get { return TehnoparkRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = TehnoparkRuCatalogPageContentParser.IsListPage(content.Document);
            if (isListPage)
            {
                var listPage = page.Site.GetPage(page.Uri, TehnoparkRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            return ParseCategories(page, content);
        }

        private static WebPageContentParsingResult ParseCategories(WebPage page, HtmlPageContent content)
        {
            HtmlNode categoriesList =
                content
                    .GetSingleNode(@"//div[@class='category-holder']");
            HtmlNodeCollection categoriesNodes =
                categoriesList
                    .GetNodes(@".//ul[@class='category-nav']/li/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = categoryNode.GetUri(page);
                var categoryPage = page.Site.GetPage(uri, TehnoparkRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}