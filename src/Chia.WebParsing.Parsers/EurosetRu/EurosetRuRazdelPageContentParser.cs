using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EurosetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    internal class EurosetRuRazdelPageContentParser : EurosetRuHtmlPageContentParser, IEurosetRuWebPageContentParser
    {
        public EurosetRuWebPageType PageType
        {
            get { return EurosetRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@class='header clearfix']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, EurosetRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNodeCollection catalogsNodes =
              content.GetNodes(@"//div[contains(@class,'p_sponsors')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                string name = catalogsNode.GetInnerText();
                Uri uri = catalogsNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, EurosetRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}