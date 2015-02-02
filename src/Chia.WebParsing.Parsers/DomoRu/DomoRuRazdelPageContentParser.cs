using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.DomoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal class DomoRuRazdelPageContentParser : DomoRuHtmlPageContentParser, IDomoRuWebPageContentParser
    {
        public DomoRuWebPageType Type
        {
            get { return DomoRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNode catalogsListNode =
                content.SelectSingleNode(@"//div[@id='search_facet_category']");
            bool isProductListPage = catalogsListNode == null;
            if (isProductListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, DomoRuWebPageType.Catalog, page.Path);
                listPage.IsPartOfSiteMap = false;
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNodeCollection catalogsNodes =
                catalogsListNode.GetNodes(@".//ul/li/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                string name = catalogsNode
                        .GetSingleNode(@"text()")
                        .GetInnerText();
                Uri uri = catalogsNode
                    .GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, DomoRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}