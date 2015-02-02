using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TenIRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TenIRu
{
    internal class TenIRuMainPageContentParser : TenIRuHtmlPageContentParser, ITenIRuWebPageContentParser
    {
        public TenIRuWebPageType PageType
        {
            get { return TenIRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//ul[@class='categories']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//li[@id]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                Uri uri = razdelNode.GetUri(page);
                string name = razdelNode.GetInnerText();
                var path = page.Path.Add(name);
                var razdelPage = page.Site.GetPage(uri, TenIRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}