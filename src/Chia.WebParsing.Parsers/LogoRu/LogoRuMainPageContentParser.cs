using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.LogoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.LogoRu
{
    internal class LogoRuMainPageContentParser : LogoRuHtmlPageContentParser, ILogoRuWebPageContentParser
    {
        public LogoRuWebPageType PageType
        {
            get { return LogoRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//div[@class='mainMenu']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//a[@class='wrap']");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemsNode in menuItemsNodes)
            {
                string name = menuItemsNode
                    .GetSingleNode(@".//span[@class='name']")
                    .GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = menuItemsNode.GetUri(page);
                WebPage catalogPage = page.Site.GetPage(uri, LogoRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);

            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}