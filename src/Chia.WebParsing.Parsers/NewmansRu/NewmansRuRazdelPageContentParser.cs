using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.NewmansRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NewmansRu
{
    internal class NewmansRuRazdelPageContentParser : NewmansRuHtmlPageContentParser, INewmansRuWebPageContentParser
    {
        public NewmansRuWebPageType PageType
        {
            get { return NewmansRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//div[contains(@class,'filter-goods')]");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, NewmansRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode razdelsList =
                content.GetSingleNode(@"//div[@class='catalog-goods']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//div[@class='l1 product section']//a[@class='category']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = razdelNode.GetUri(page);
                var razdelPage = page.Site.GetPage(uri, NewmansRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}