using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TechnonetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal class TechnonetRuRazdelPageContentParser : TechnonetRuHtmlPageContentParser, ITechnonetRuWebPageContentParser
    {
        public TechnonetRuWebPageType PageType
        {
            get { return TechnonetRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@class='filtren']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, TechnonetRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode categoriesList =
                content.GetSingleNode(@"//div[@class='b-listcatsecond']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//p[@class='b-listcatsecond-name']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoriesNode in categoriesNodes)
            {
                string name = categoriesNode.GetInnerText();
                Uri uri = categoriesNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, TechnonetRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}