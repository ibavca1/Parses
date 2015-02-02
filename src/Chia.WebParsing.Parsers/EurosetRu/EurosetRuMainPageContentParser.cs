using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EurosetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    internal class EurosetRuMainPageContentParser : EurosetRuHtmlPageContentParser, IEurosetRuWebPageContentParser
    {
        public EurosetRuWebPageType PageType
        {
            get { return EurosetRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//div[@class='nav']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(".//ul/li[@id]/a[@class='root-icon']");
            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemsNode in menuItemsNodes)
            {
                string name = menuItemsNode.GetInnerText();
                Uri uri = menuItemsNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, EurosetRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}