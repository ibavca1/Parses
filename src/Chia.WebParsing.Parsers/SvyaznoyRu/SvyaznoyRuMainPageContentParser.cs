using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.SvyaznoyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    internal class SvyaznoyRuMainPageContentParser : SvyaznoyRuHtmlPageContentParser, ISvyaznoyRuWebPageContentParser
    {
        public SvyaznoyRuWebPageType PageType
        {
            get { return SvyaznoyRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//div[@class='b-menu__menu']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//li[@class='menu1-list__bl ']/a");
            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                string name = menuItemNode.GetSingleNode(@"span").GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = menuItemNode.GetUri(page);
                var catalogPage = page.Site.GetPage(uri, SvyaznoyRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}