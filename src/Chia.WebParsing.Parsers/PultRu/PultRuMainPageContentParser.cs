using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.PultRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.PultRu
{
    internal class PultRuMainPageContentParser : PultRuHtmlPageContentParser, IPultRuWebPageContentParser
    {
        public PultRuWebPageType PageType
        {
            get { return PultRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode mainMenuNode =
                content.GetSingleNode(@"//div[@class='catalog-inner menu']");
            HtmlNodeCollection menuItemsNodes =
                mainMenuNode.GetNodes(@"ul/li/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in menuItemsNodes)
            {
                string name = categoryNode.GetSingleNode(@"span").GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, PultRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}