using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.NewmansRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NewmansRu
{
    internal class NewmansRuMainPageContentParser : NewmansRuHtmlPageContentParser, INewmansRuWebPageContentParser
    {
        public NewmansRuWebPageType PageType
        {
            get { return NewmansRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//ul[@class='maimlevel']");
            HtmlNodeCollection menuItemNodes =
                menuNode.GetNodes(@".//li[@idsm]/a[not(text()='')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemNodes)
            {
                string name = menuItemNode.GetInnerText();
                var path = page.Path.Add(name);
                Uri uri = menuItemNode.GetUri(page);
                var menuItemPage = page.Site.GetPage(uri, NewmansRuWebPageType.Razdel, path);
                menuItemPage.IsPartOfSiteMap = true;
                pages.Add(menuItemPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}