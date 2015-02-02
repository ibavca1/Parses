using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.UtinetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    internal class UtinetRuMainPageContentParser : UtinetRuHtmlPageContentParser, IUtinetRuWebPageContentParser
    {
        public UtinetRuWebPageType PageType
        {
            get { return UtinetRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            
            HtmlNode razdelsList = content
                .GetSingleNode(@"//ul[@id='revolverShowCaseList']");
            HtmlNodeCollection razdelsNodes = razdelsList
                .GetNodes(@".//a[@class='item image-link']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, UtinetRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}