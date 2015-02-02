using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.OnlinetradeRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal class OnlinetradeRuRazdelsListPageContentParser : OnlinetradeRuHtmlPageContentParser, IOnlinetradeRuWebPageContentParser
    {
        public OnlinetradeRuWebPageType PageType
        {
            get { return OnlinetradeRuWebPageType.RazdelsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList = 
                content.GetSingleNode(@"//div[@class='gim_container']");
            HtmlNodeCollection razdelsNodes = 
                razdelsList.GetNodes(@".//div[@class='gim_first_level']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode
                    .GetSingleNode(@".//div[@class='gim_title']/a")
                    .GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);

                WebPage[] categoriesPages = ParseCategories(page, razdelNode, path).ToArray();
                pages.AddRange(categoriesPages);
            }

            pages.ForEach(p => p.IsPartOfSiteMap = true);
            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlNode razdelNode, WebSiteMapPath razdelPath)
        {
            HtmlNodeCollection categoriesNodes =
                razdelNode.GetNodes(@".//div[@class='gim_parent']");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                Uri uri = categoryNode
                    .GetSingleNode(@".//div[@class='gim_title']/a")
                    .GetUri(page);
                string name = categoryNode
                    .GetSingleNode(@".//div[@class='gim_title']/a")
                    .GetInnerText();
                WebSiteMapPath path = razdelPath.Add(name);

                WebPage[] subCategoriesPages = ParseSubCategories(page, categoryNode, path).ToArray();
                if (subCategoriesPages.Any())
                {
                    pages.AddRange(subCategoriesPages);
                    continue;
                }

                var categoryPage = page.Site.GetPage(uri, OnlinetradeRuWebPageType.Razdel, path);
                pages.Add(categoryPage);
            }

            return pages;
        }

        private static IEnumerable<WebPage> ParseSubCategories(WebPage page, HtmlNode categoryNode, WebSiteMapPath categoryPath)
        {
            HtmlNodeCollection subCategoriesNodes =
                categoryNode.SelectNodes(@".//div[@class='gim_children']/a");
            if (subCategoriesNodes == null)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode subCategoryNode in subCategoriesNodes)
            {
                string name = subCategoryNode.GetInnerText();
                Uri uri = subCategoryNode.GetUri(page);
                WebSiteMapPath path = categoryPath.Add(name);

                WebPage categoryPage = page.Site.GetPage(uri, OnlinetradeRuWebPageType.Razdel, path);
                pages.Add(categoryPage);
            }

            return pages;
        }
    }
}