using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.MtsRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MtsRu
{
    internal class MtsRuMainPageContentParser : HtmlPageContentParser, IMtsRuWebPageContentParser
    {
        public MtsRuWebPageType PageType
        {
            get { return MtsRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//ul[@class='b-left_submenu']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//li//a[not(@id)]");
            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                string name = menuItemNode.GetInnerText();
                Uri uri = menuItemNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage itemPage = page.Site.GetPage(uri, MtsRuWebPageType.Razdel, path);
                itemPage.IsPartOfSiteMap = true;
                pages.Add(itemPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}