using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TehnosilaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal class TehnosilaRuRazdelPageContentParser : TehnosilaRuHtmlPageContentParser, ITehnosilaRuWebPageContentParser
    {
        public TehnosilaRuWebPageType PageType
        {
            get { return TehnosilaRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode catalogList = content
                .GetSingleNode(@"//div[@class='subcategories']/div[@class='list']");
            HtmlNodeCollection catalogNodes = catalogList
                .GetNodes(@".//div[contains(@class,'item')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogNode in catalogNodes)
            {
                WebPage[] categoryPages = ParseCategoryNode(catalogNode, page).ToArray();
                pages.AddRange(categoryPages);
            }

            pages.ForEach(p => p.IsPartOfSiteMap = true);
            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseCategoryNode(HtmlNode catalogNode, WebPage page)
        {
            WebPage[] subCategoriesPages = ParseSubCategories(catalogNode, page).ToArray();
            if (subCategoriesPages.Any())
                return subCategoriesPages;

            HtmlNode titleNode = catalogNode
                .GetSingleNode(@".//div[@class='title']/a");
            Uri uri = titleNode.GetUri(page);
            string name = titleNode.GetInnerText();
            WebSiteMapPath path = page.Path.Add(name);
            return new[] {page.Site.GetPage(uri, TehnosilaRuWebPageType.Catalog, path)};
        }

        private static IEnumerable<WebPage> ParseSubCategories(HtmlNode catalogNode, WebPage page)
        {
            string categoryName = catalogNode
                .GetSingleNode(@".//div[@class='title']/a")
                .GetInnerText();

            HtmlNodeCollection subCategoriesNodes = 
                catalogNode.SelectNodes(@".//div[@class='tab-contents']/a");
            bool noSubCategories = subCategoriesNodes == null;
            if (noSubCategories)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode subCategoryNode in subCategoriesNodes)
            {
                string name = subCategoryNode.GetInnerText();
                Uri uri = subCategoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(categoryName, name);
                WebPage subCategoryPage = page.Site.GetPage(uri, TehnosilaRuWebPageType.Catalog, path);
                pages.Add(subCategoryPage);
            }

            return pages;
        }
    }
}