using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.RbtRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RbtRu
{
    internal class RbtRuMainPageContentParser : RbtRuHtmlPageContentParser, IRbtRuWebPageContentParser
    {
        public RbtRuWebPageType PageType
        {
            get { return RbtRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode mainMenuNode =
                content.GetSingleNode(@"//div[@id='mainMenu']");
            HtmlNodeCollection menuItemsNodes =
                mainMenuNode.GetNodes(@".//div[@class='catalogBlock']/div[@class='info']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in menuItemsNodes)
            {
                string name = categoryNode.GetInnerText();
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