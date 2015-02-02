using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CorpCentreRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal class CorpCentreRuRazdelPageContentParser : CorpCentreRuHtmlPageContentParser, ICorpCentreRuWebPageContentParser
    {
        public CorpCentreRuWebPageType PageType
        {
            get { return CorpCentreRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@class='catalog-filter']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, CorpCentreRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode catalogList =
                content.GetSingleNode(@"//div[contains(@class,'catalog-section-list')]");
            HtmlNodeCollection catalogsNodes =
                catalogList.GetNodes(@".//div[@class='section-item']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                string name = catalogsNode.GetAttributeText("title");
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = catalogsNode.GetUri(page);
                WebPage catalogPage = page.Site.GetPage(uri, CorpCentreRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}