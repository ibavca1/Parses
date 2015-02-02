using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CentrBtRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    internal class CentrBtRuMainPageContentParser : CentrBtRuHtmlPageContentParser, ICentrBtRuWebPageContentParser
    {
        public virtual CentrBtRuWebPageType PageType
        {
            get { return CentrBtRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            return ParseRazdels(page, content);
        }

        protected static WebPageContentParsingResult ParseRazdels(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection razdelsNodes =
                content.GetNodes(@"//span[@class='catalog_title']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.ParentNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, CentrBtRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}