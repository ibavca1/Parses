using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnoshokRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal class TehnoshokRuRazdelPageContentParser : TehnoshokRuHtmlPageContentParser, ITehnoshokRuWebPageContentParser
    {
        public TehnoshokRuWebPageType PageType
        {
            get { return TehnoshokRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = TehnoshokRuCatalogPageContentParser.IsListPage(content);
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, TehnoshokRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNodeCollection categoriesNodes =
                content.GetNodes(@"//div[@class='papers-content']/h3/a");
            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                Uri uri = categoryNode.GetUri(page);
                string name = categoryNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, TehnoshokRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}