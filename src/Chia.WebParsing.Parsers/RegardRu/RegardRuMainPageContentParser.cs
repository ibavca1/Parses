using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.RegardRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RegardRu
{
    internal class RegardRuMainPageContentParser : RegardRuHtmlPageContentParser, IRegardRuWebPageContentParser
    {
        public RegardRuWebPageType PageType
        {
            get { return RegardRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//div[@id='lmenu']/ul[contains(@class,'menu')]");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@"li[@class]");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode
                    .GetSingleNode(@"a")
                    .GetInnerText();
                Uri uri = razdelNode
                    .GetSingleNode(@"a")
                    .GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);

                WebPage[] categoriesPages = ParseCategories(page, razdelNode, path).ToArray();
                if (categoriesPages.Any())
                {
                    pages.AddRange(categoriesPages);
                    continue;
                }

                WebPage razdelPage = page.Site.GetPage(uri, RegardRuWebPageType.Catalog, path);
                pages.Add(razdelPage);
            }

            pages.ForEach(p => p.IsPartOfSiteMap = true);
            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlNode razdelNode, WebSiteMapPath razdelPath)
        {
            HtmlNodeCollection categoriesNodes =
                razdelNode.SelectNodes(@"ul/li[@class]");
            if (categoriesNodes == null)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode
                    .GetSingleNode(@"a")
                    .GetInnerText();
                Uri uri = categoryNode
                    .GetSingleNode(@"a")
                    .GetUri(page);
                WebSiteMapPath path = razdelPath.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, RegardRuWebPageType.Catalog, path);
                pages.Add(categoryPage);
            }

            return pages;
        }
    }
}