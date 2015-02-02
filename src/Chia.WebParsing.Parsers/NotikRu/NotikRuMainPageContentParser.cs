using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.NotikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NotikRu
{
    internal class NotikRuMainPageContentParser : NotikRuHtmlPageContentParser, INotikRuWebPageContentParser
    {
        public NotikRuWebPageType PageType
        {
            get { return NotikRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//div[@class='TopMenu']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in menuItemsNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, NotikRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}