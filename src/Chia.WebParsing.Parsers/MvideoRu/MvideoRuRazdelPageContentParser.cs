using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.MvideoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal class MvideoRuRazdelPageContentParser : MvideoRuHtmlPageContentParser, IMvideoRuWebPageContentParser
    {
        public MvideoRuWebPageType Type
        {
            get { return MvideoRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@class='pagination-section']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, MvideoRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode razdelsList =
                content.GetSingleNode(@"//ul[@class='category-grid']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//a[@class='category-grid-item-link']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                Uri uri = razdelNode.GetUri(page);
                string name = razdelNode
                    .GetSingleNode(@".//div[@class='category-grid-title']")
                    .GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, MvideoRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}