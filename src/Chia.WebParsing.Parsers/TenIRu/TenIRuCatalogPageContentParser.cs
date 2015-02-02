using System;
using Chia.WebParsing.Companies.TenIRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TenIRu
{
    internal class TenIRuCatalogPageContentParser : TenIRuHtmlPageContentParser, ITenIRuWebPageContentParser
    {
        public TenIRuWebPageType PageType
        {
            get { return TenIRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/ItemList']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//div[@itemtype='http://schema.org/Product']");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                string article = productNode
                    .GetSingleNode(@".//div[@class='article']")
                    .GetDigitsText();
                string name = productNode
                    .GetSingleNode(@".//div[@class='title']/a")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//div[@class='title']/a")
                    .GetUri(page);
                decimal price = productNode
                    .GetSingleNode(@".//meta[@itemprop='price']")
                    .GetPrice("content");

                var product = new WebMonitoringPosition(article, name, price, uri);
                result.Positions.Add(product);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//div[@class='product-page']");
            if (pagingNode == null)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@"div[contains(@class,'page-selected')]/following-sibling::div/a");
            bool noMorePages = nextPageNode == null;
            if (noMorePages)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, TenIRuWebPageType.Catalog, page.Path);
        } 
    }
}