using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.DnsShopRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal class DnsShopRuRazdelPageContentParser : DnsShopRuHtmlPageContentParser, IDnsShopRuWebPageContentParser
    {
        public DnsShopRuWebPageType Type
        {
            get { return DnsShopRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode catalogNode = content
                .GetSingleNode(@"//ul[@id='index_catalog_list']");
            WebPage[] pages = ParseRazdels(page, page.Path, catalogNode, 0).ToArray();
            Array.ForEach(pages, p => p.IsPartOfSiteMap = true);
            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseRazdels(WebPage page, WebSiteMapPath path, HtmlNode node, int level)
        {
            string xPathQuery = 
                string.Format(@".//li[contains(@class,'level_{0}')]", level);
            HtmlNodeCollection razdelsNodes =
                node.SelectNodes(xPathQuery);
            bool noRazdels = razdelsNodes == null;
            if (noRazdels)
                return Enumerable.Empty<WebPage>();

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                WebPage[] razdelPages = ParseRazdelNode(page, razdelNode, level, path).ToArray();
                pages.AddRange(razdelPages);
            }

            return pages;
        }

        private static IEnumerable<WebPage> ParseRazdelNode(WebPage page, HtmlNode razdelNode, int level, WebSiteMapPath path)
        {
            HtmlNode nameNode = razdelNode
                .GetSingleNode(@"a");
            string razdelName = nameNode.GetInnerText();
            if (string.IsNullOrWhiteSpace(razdelName))
                return Enumerable.Empty<WebPage>();

            Uri razdelUri = nameNode.GetUri(page);
            WebSiteMapPath razdelPath = path.Add(razdelName);

            WebPage[] subRazdelsPages = ParseRazdels(page, razdelPath, razdelNode, level + 1).ToArray();
            bool noSubRazdels = subRazdelsPages.Length ==0;
            if (noSubRazdels)
            {
                WebPage razdelPage = page.Site.GetPage(razdelUri, DnsShopRuWebPageType.Catalog, razdelPath);
                return new[] {razdelPage};
            }

            return subRazdelsPages;
        }
    }
}