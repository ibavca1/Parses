using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EldoradoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal class EldoradoRuRazdelPageContentParser : EldoradoRuHtmlPageContentParser, IEldoradoRuWebPageContentParser
    {
        public EldoradoRuWebPageType Type
        {
            get { return EldoradoRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@id='filtered-goods-list']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, EldoradoRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode noItemsNode =
                content.SelectSingleNode(@"//div[@class='noItem']");
            bool noItems = noItemsNode != null;
            if (noItems)
                return WebPageContentParsingResult.Empty;

            HtmlNodeCollection catalogNodes =
                content.GetNodes(@"//a[contains(@class,'mainHitsSection')]");
            var pages = new List<WebPage>();
            foreach (HtmlNode catalogNode in catalogNodes)
            {
                Uri uri = catalogNode.GetUri(page);
                string name = catalogNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, EldoradoRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}