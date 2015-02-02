using System;
using Chia.WebParsing.Companies.MvideoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal class MvideoRuCatalogPageContentParser : MvideoRuHtmlPageContentParser, IMvideoRuWebPageContentParser
    {
        public MvideoRuWebPageType Type
        {
            get { return MvideoRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode noResultsNode =
                content.SelectSingleNode(@"//div[@class='search-no-results-description']");
            bool noResults = noResultsNode != null;
            if (noResults)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
            {
                result.Pages.Add(nextPage);
            }

            return result;
        }

        internal static bool IsListPage(HtmlPageContent content)
        {
            HtmlNode paginationNode =
                content.SelectSingleNode(@"//div[@class='pagination-section']");
            HtmlNode galleryNode =
                content.SelectSingleNode(@"//li[@class='content-landing-article']");
            return paginationNode != null || galleryNode != null;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            var result = new WebPageContentParsingResult();

            HtmlNodeCollection goodsNodes =
                content.GetNodes(@"//div[contains(@class,'product-tile-info')]/..");
            foreach (HtmlNode goodsNode in goodsNodes)
            {
                HtmlNode actionNode =
                    goodsNode.SelectSingleNode(@".//div[@class='product-badge']");
                HtmlNode nameAndLinkNode =
                    goodsNode.GetSingleNode(@".//a[@class='product-tile-title-link']");

                // бывает 'В настоящее время товар не доступен для покупки на сайте.'
                // бывает 'В настоящее время товар недоступен для покупки на сайте.'
                bool isAvailableForBuy =
                    goodsNode.DoesNotHaveNode(@".//*[contains(text(),'В настоящее время товар не')]");
                bool isAction = actionNode != null;
                bool isPresent =
                    goodsNode.DoesNotHaveNode(
                        @".//*[contains(text(),'Товар временно отсутствует в продаже') or contains(text(),'Товар распродан')]");
                string name = nameAndLinkNode.GetInnerText();
                Uri uri = nameAndLinkNode.GetUri(page);
                decimal onlinePrice = 0;
                decimal retailPrice = 0;

                if (isPresent && isAvailableForBuy)
                {
                    retailPrice = goodsNode
                        .GetSingleNode(@".//strong[@class='product-price-current']")
                        .GetPrice();
                    bool isRetailOnly =
                        goodsNode.HasNode(
                            @".//*[contains(text(),'Этот товар можно купить только в розничных магазинах')]");
                    if (!isRetailOnly)
                    {
                        onlinePrice = retailPrice;
                    }
                }

                bool needToGoToProductPage =
                    name.EndsWith("...") ||
                    options.ProductArticle ||
                    options.AvailabiltyInShops;

                if (needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, MvideoRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                    continue;
                }

                var position = new WebMonitoringPosition(null, name, onlinePrice, retailPrice, uri)
                {
                    IsAction = isAction
                };
                result.Positions.Add(position);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//div[@class='pagination pagination-centered']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@"a[contains(@class,'ico-pagination-next') and @href]");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, MvideoRuWebPageType.Catalog, page.Path);
        }

    }
}