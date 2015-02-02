using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.PultRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.PultRu
{
    internal class PultRuRazdelPageContentParser : PultRuHtmlPageContentParser, IPultRuWebPageContentParser
    {
        public PultRuWebPageType PageType
        {
            get { return PultRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode catalogsList =
                content.GetSingleNode(@"//div[@class='category']");
            HtmlNodeCollection catalogsNodes =
                catalogsList.GetNodes(@".//div[contains(@class,'subcat')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in catalogsNodes)
            {
                Uri uri = categoryNode.GetUri(page);
                if (uri.IsSame(page.Uri))
                    continue;

                string name = categoryNode.GetSingleNode(@"span").GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, PultRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}