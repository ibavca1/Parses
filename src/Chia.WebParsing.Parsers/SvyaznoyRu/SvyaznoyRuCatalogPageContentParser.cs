using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.SvyaznoyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    internal class SvyaznoyRuCatalogPageContentParser : SvyaznoyRuHtmlPageContentParser, ISvyaznoyRuWebPageContentParser
    {
        public SvyaznoyRuWebPageType PageType
        {
            get { return SvyaznoyRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[@id='listLine']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@"//div[@itemtype='http://schema.org/ImageObject']");

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productsNodes)
            {
                Uri uri =
                    productNode
                        .GetSingleNode(@".//*[@itemprop='name']")
                        .GetUri(page);

                var productPage = page.Site.GetPage(uri, SvyaznoyRuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult { Pages =  pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode pagingNode =
                content.SelectSingleNode(@"//div[@class='pagesPage']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode = pagingNode.SelectSingleNode(@".//div[@class='forv']/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, SvyaznoyRuWebPageType.Catalog, page.Path);
        }
    }
}