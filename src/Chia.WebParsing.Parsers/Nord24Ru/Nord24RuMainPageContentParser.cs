using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.Nord24Ru;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    internal class Nord24RuMainPageContentParser : Nord24RuHtmlPageContentParser, INord24RuWebPageContentParser
    {
        public Nord24RuWebPageType PageType
        {
            get { return Nord24RuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.SelectSingleNode(@"//div[contains(@class,'shopMenuLine')]");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//a[@class='wrap']");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemsNode in menuItemsNodes)
            {
                string name = 
                    menuItemsNode.GetSingleNode(@".//div[@class='name']").GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = menuItemsNode.GetUri(page);
                WebPage catalogPage = page.Site.GetPage(uri, Nord24RuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);

            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}