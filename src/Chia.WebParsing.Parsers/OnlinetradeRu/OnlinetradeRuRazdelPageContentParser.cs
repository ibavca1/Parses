using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.OnlinetradeRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal class OnlinetradeRuRazdelPageContentParser : OnlinetradeRuHtmlPageContentParser, IOnlinetradeRuWebPageContentParser
    {
        public OnlinetradeRuWebPageType PageType
        {
            get { return OnlinetradeRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//div[@class='pre_chars']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, OnlinetradeRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode razdelsList =
                content.GetSingleNode(@"//div[@class='gscm_container']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//div[@class='gscm_title']/a");
            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                Uri uri = razdelNode.GetUri(page);
                var categoryPage = page.Site.GetPage(uri, OnlinetradeRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages }; 

        }
    }
}