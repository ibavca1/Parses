using System;
using Chia.WebParsing.Companies.RbtRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RbtRu
{
    internal class RbtRuCatalogPageContentParser : RbtRuHtmlPageContentParser, IRbtRuWebPageContentParser
    {
        public RbtRuWebPageType PageType
        {
            get { return RbtRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool noPositions = content.HasNode(@"//div[@class='NotFound']");
            if (noPositions)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[contains(@class, 'catalogList-horizont')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//div[@itemtype='http://schema.org/Product']");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                string tradeGroup = productNode
                    .GetSingleNode(@".//span[@class='og_block_subheader']")
                    .GetInnerText();
                string modelName = productNode
                    .GetSingleNode(@".//span[@itemprop='name']")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//a[@itemprop='url']")
                    .GetUri(page);
                decimal price = productNode
                    .GetSingleNode(@".//meta[@itemprop='price']")
                    .GetPrice("content");

                bool needToGoToProductPage = options.AvailabiltyInShops || modelName.EndsWith("…");
                if (needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, RbtRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                }
                else
                {
                    string fullName = string.Format("{0} {1}", tradeGroup, modelName).Trim();
                    var product = new WebMonitoringPosition(null, fullName, price, uri);
                    result.Positions.Add(product);
                }
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//div[@class='paginate']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[@class='next' and not(@href='')]");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, RbtRuWebPageType.Catalog, page.Path);
        }
    }
}