using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.OldiRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OldiRu
{
    internal class OldiRuMainPageContentParser : OldiRuHtmlPageContentParser, IOldiRuWebPageContentParser
    {
        public OldiRuWebPageType PageType
        {
            get { return OldiRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode navigationNode =
                content.GetSingleNode(@"//div[@class='navigation']");
            HtmlNodeCollection menuItemsNodes =
                navigationNode.GetNodes(@".//li[@class='ol-menu-main-level']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                Uri uri = menuItemNode.GetUri(page);
                string name = menuItemNode
                    .GetSingleNode("div")
                    .GetInnerText();
                var path = page.Path.Add(name);
                var razdelPage = page.Site.GetPage(uri, OldiRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}