using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.KeyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal class KeyRuRazdelPageContentParser : KeyRuHtmlPageContentParser, IKeyRuWebPageContentParser
    {
        public KeyRuWebPageType Type
        {
            get { return KeyRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            WebPage[] mainCatalogsPages = ParseMainCatalogs(page, content).ToArray();
            pages.AddRange(mainCatalogsPages);

            WebPage[] secondaryCatalogsPages = ParseSecondaryCatalogs( page, content).ToArray();
            pages.AddRange(secondaryCatalogsPages);

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseMainCatalogs(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection catalogsNodes = content
                .GetNodes(
                    @"//section[@class='catalogpage_main_block common_items_block mb55']//div[@class='title_block']");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                HtmlNode uriNode =
                    catalogsNode.SelectSingleNode(@"a");
                if (uriNode == null)
                    continue;

                string name = catalogsNode
                        .GetSingleNode(@"h3")
                        .GetInnerText();
                Uri uri = uriNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, KeyRuWebPageType.Catalog, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return pages;
        }

        private static IEnumerable<WebPage> ParseSecondaryCatalogs(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection catalogsNodes = content
                .SelectNodes(@"//div[@class='span1 catalog_4pic_block']");
            if (catalogsNodes == null)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                Uri uri = catalogsNode
                    .GetSingleNode(@".//div[@class='title_block']/a")
                    .GetUri(page);
                string name = catalogsNode
                    .SelectSingleNode(@".//div[@class='title_block']/a/span")
                    .GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, KeyRuWebPageType.Catalog, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return pages;
        }
    }
}