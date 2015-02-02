using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TenIRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TenIRu
{
    internal class TenIRuRazdelPageContentParser : TenIRuHtmlPageContentParser, ITenIRuWebPageContentParser
    {
        public TenIRuWebPageType PageType
        {
            get { return TenIRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//div[@id='filter']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, TenIRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNodeCollection razdelsNodes =
                content.GetNodes(@"//div[contains(@class,'js-catalog-name')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                Uri uri = razdelNode.GetUri(page);
                string name = razdelNode.GetInnerText();
                name = RemoveCountFromName(name);
                var path = page.Path.Add(name);
                var razdelPage = page.Site.GetPage(uri, TenIRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }

        private static string RemoveCountFromName(string name)
        {
            int index = name.LastIndexOf('(');
            return index != -1 ? name.Substring(0, index).Trim() : name;
        }
    }
}