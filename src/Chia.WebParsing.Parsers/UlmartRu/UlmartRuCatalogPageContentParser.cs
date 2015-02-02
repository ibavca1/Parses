using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using Chia.WebParsing.Companies.UlmartRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal class UlmartRuCatalogPageContentParser : HtmlPageContentParser, IUlmartRuWebPageContentParser
    {
        public UlmartRuWebPageType PageType
        {
            get { return UlmartRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isEmpty =
                content.HasNode(
                    @"//div[@id='catalogGoodsBlock']//p[contains(text(),'К сожалению, в данной категории нет товаров')]");
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content, context);
            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[@id='catalogGoodsBlock']");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(@".//section[@data-fly-object]");

            bool noProducts = productsNodes == null;
            if (noProducts)
                return WebPageContentParsingResult.Empty;

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                WebPageContentParsingResult product = ParseProduct(page, productNode, context.Options, content.IsJavaScriptProcessed);
                result.Add(product);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProduct(
            WebPage page, HtmlNode productNode, WebSiteParsingOptions options, bool isJavaScript)
        {
            Uri uri = productNode
                .GetSingleNode(@".//h3[@class='b-product__title']//i")
                .GetUri(page, "title");
            string name = productNode
                .GetSingleNode(@".//h3[@class='b-product__title']//i")
                .GetInnerText();
            string article = productNode
                .GetSingleNode(@".//div[@class='b-product__art']/span")
                .GetDigitsText();
            string characteristics = productNode
                .SelectSingleNode(@".//div[@class='b-product__descr']")
                .GetInnerText();
            decimal price = 0;

            bool isInReserve =
                productNode.HasNode(@".//div[contains(@class, '_inreserve')]");
            if (!isInReserve)
            {
                price = productNode
                    .GetSingleNode(@".//div[@class='b-product__price']//span[@class='b-price__num']").
                    GetPrice();
            }

            bool needToGoToProductPage = options.AvailabiltyInShops;
            if (price != 0 && needToGoToProductPage)
            {
                var productPage = page.Site.GetPage(uri, UlmartRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var position = new WebMonitoringPosition(article, name, price, uri)
                               {
                                   Characteristics = characteristics
                               };

            return WebPageContentParsingResult.FromPosition(position);
        }

        private static WebPage ParseNextPage(WebPage parsedPage, HtmlPageContent content)
        {
            NameValueCollection query = HttpUtility.ParseQueryString(parsedPage.Uri.Query);
            string pageNumberValue = query["pageNum"];
            int pageNumber =
                pageNumberValue != null ? int.Parse(pageNumberValue, NumberStyles.Number) : 1;
            int nextPageNumber = pageNumber + 1;

            Uri nextPageUri = parsedPage.Uri.AddQueryParam("pageNum", nextPageNumber.ToString());
            return parsedPage.Site.GetPage(nextPageUri, UlmartRuWebPageType.Catalog, parsedPage.Path);
        }
    }
}