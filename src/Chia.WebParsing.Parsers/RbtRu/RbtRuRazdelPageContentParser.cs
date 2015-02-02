using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.RbtRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RbtRu
{
    internal class RbtRuRazdelPageContentParser : RbtRuHtmlPageContentParser, IRbtRuWebPageContentParser
    {
        public RbtRuWebPageType PageType
        {
            get { return RbtRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@id='items-filter']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, RbtRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode categoriesList =
                content.GetSingleNode(@"//div[contains(@class,'cat-subs')]");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//div[@class='cat-sub']//h5/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode
                    .SelectSingleNode(@"text()")
                    .GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, RbtRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}