using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.HolodilnikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    internal class HolodilnikRuRazdelPageContentParser : HolodilnikRuHtmlPageContentParser, IHolodilnikRuWebPageContentParser
    {
        public virtual HolodilnikRuWebPageType PageType
        {
            get { return HolodilnikRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//form[@id='comp_form']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, HolodilnikRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }
            
            HtmlNodeCollection catalogNodes =
                content.GetNodes(
                    @"//tr[2]/td[1]/ul[contains(@class,'ar')]/li[contains(@class,'pv5')]/b/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogNode in catalogNodes)
            {
                Uri uri = catalogNode.GetUri(page);
                string name = catalogNode.GetInnerText();
                if (string.IsNullOrWhiteSpace(name))
                    continue;
                WebSiteMapPath path = page.Path.Add(name);

                WebPage catalogPage = page.Site.GetPage(uri, HolodilnikRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}