using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnoparkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    internal class TehnoparkRuMainPageContentParser : TehnoparkRuHtmlPageContentParser, ITehnoparkRuWebPageContentParser
    {
        public TehnoparkRuWebPageType PageType
        {
            get { return TehnoparkRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList = content
                .GetSingleNode(@"//ul[@id='category-nav']");
            HtmlNodeCollection razdelsNodes = razdelsList
                .GetNodes(@".//li[contains(@class,'lev_1') and contains(@class,'sec')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                Uri uri = razdelNode
                    .GetSingleNode(@"a")
                    .GetUri(page);
                string name = razdelNode
                    .GetSingleNode(@".//div[@class='m-m-s']/span[@class='holder']")
                    .GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, TehnoparkRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}