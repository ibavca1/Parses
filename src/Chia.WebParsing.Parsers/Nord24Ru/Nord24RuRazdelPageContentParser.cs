using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EurosetRu;
using Chia.WebParsing.Companies.Nord24Ru;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    internal class Nord24RuRazdelPageContentParser : Nord24RuHtmlPageContentParser, INord24RuWebPageContentParser
    {
        public Nord24RuWebPageType PageType
        {
            get { return Nord24RuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[contains(@class,'selectOfPatam')]");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, Nord24RuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode catalogsList =
                content.GetSingleNode(@"//div[@class='tallyList']");
            HtmlNodeCollection catalogsNodes =
                catalogsList.GetNodes(@".//div[contains(@class,'itempr')]/a");

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