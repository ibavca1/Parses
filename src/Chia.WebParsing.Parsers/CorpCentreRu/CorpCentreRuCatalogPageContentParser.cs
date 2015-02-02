using System;
using Chia.WebParsing.Companies.CorpCentreRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal class CorpCentreRuCatalogPageContentParser : CorpCentreRuHtmlPageContentParser, ICorpCentreRuWebPageContentParser
    {
        public CorpCentreRuWebPageType PageType
        {
            get { return CorpCentreRuWebPageType.Catalog; }
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

       private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[contains(@class,'catalog-list')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//div[contains(@class,'catalog-item')]");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                WebPageContentParsingResult product = ParseProduct(page, productNode, options);
                result.Add(product);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProduct(WebPage page, HtmlNode productNode, WebSiteParsingOptions options)
        {
            string article = null;
            string name =
                productNode
                    .GetSingleNode(@".//div[@class='name']/a")
                    .GetInnerText();
            Uri uri = productNode
                    .GetSingleNode(@".//div[@class='name']/a")
                    .GetUri(page);
            decimal onlinePrice = 0;
            decimal retailPrice = 0;

            bool isOutOfStock =
                productNode.HasNode(@".//div[@class='available']//span[@class='noisset']");
            if (!isOutOfStock)
            {
                onlinePrice =
                    productNode
                        .GetSingleNode(@".//div[@class='price']/div/strong/span")
                        .GetPrice();
            }

            bool isOnlineOnly =
                productNode.HasNode(@".//div[@class='available']//span[@class='wait']");
            if (!isOnlineOnly)
            {
                retailPrice = onlinePrice;
            }

            bool isDailyHit =
                productNode.HasNode(@".//div[@class='goodday-info']");
            if (!isDailyHit)
            {
                article =
                    productNode
                        .GetSingleNode(@".//div[@class='code']")
                        .GetDigitsText();
            }

            bool needToGoToProductPage = isDailyHit || options.AvailabiltyInShops;
            if (needToGoToProductPage)
            {
                WebPage productPage = page.Site.GetPage(uri, CorpCentreRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//div[contains(@class,'catalog-pagenav')]");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[@class='next']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, CorpCentreRuWebPageType.Catalog, page.Path);
        }

        
    }
}