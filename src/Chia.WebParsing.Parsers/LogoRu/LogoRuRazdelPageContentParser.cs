using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.LogoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.LogoRu
{
    internal class LogoRuRazdelPageContentParser : LogoRuHtmlPageContentParser, ILogoRuWebPageContentParser
    {
        public LogoRuWebPageType PageType
        {
            get { return LogoRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//div[contains(@class,'selectOfPatam')]");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, LogoRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode catalogNode =
                content.GetSingleNode(@"//div[@class='tallyList']");
            HtmlNodeCollection catalogsNodes =
                catalogNode.GetNodes(@".//div[contains(@class,'itempr')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                string name = catalogsNode.GetInnerText();
                Uri uri = catalogsNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, LogoRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}