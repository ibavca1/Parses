using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CorpCentreRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal class CorpCentreRuRazdelsListPageContentParser: CorpCentreRuHtmlPageContentParser, ICorpCentreRuWebPageContentParser
    {
        public CorpCentreRuWebPageType PageType
        {
            get { return CorpCentreRuWebPageType.RazdelsList; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//ul[@class='catalog']");
            HtmlNodeCollection menuItemsNodes =
                menuNode.GetNodes(@".//li[contains(@class,'parent')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemsNode in menuItemsNodes)
            {
                Uri uri = menuItemsNode.GetUri(page);
                string name =
                    menuItemsNode.GetSingleNode(@"span").GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage menuItemsPage = page.Site.GetPage(uri, CorpCentreRuWebPageType.Razdel, path);
                menuItemsPage.IsPartOfSiteMap = true;
                pages.Add(menuItemsPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}