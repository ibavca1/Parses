using System;
using Chia.WebParsing.Companies.TdPoiskRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal class TdPoiskRuCatalogPageContentParser : TdPoiskRuHtmlPageContentParser, ITdPoiskRuWebPageContentParser
    {
        public TdPoiskRuWebPageType PageType
        {
            get { return TdPoiskRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//ul[@class='b-catalog b-catalog_custom']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//li[contains(@class,'b-catalog__item')]");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                WebPageContentParsingResult product = ParseProduct(page, productNode, options);
                result.Add(product);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProduct(
            WebPage page, HtmlNode productNode, WebSiteParsingOptions options)
        {
            string name =
                productNode.GetSingleNode(@".//div[@class='b-catalog__name']/a").GetInnerText();
            Uri uri =
                productNode.GetSingleNode(@".//div[@class='b-catalog__name']/a").GetUri(page);
            decimal retailPrice = 
                productNode.GetSingleNode(@".//div[@class='b-price b-hot-price']").GetPrice();
            decimal onlinePrice = 0;

            bool isRetailOnly =
                productNode.HasNode(".//*[text()='Только в розничных магазинах']");
            if (!isRetailOnly)
            {
                onlinePrice = retailPrice;
            }

            bool needToGoToProductPage =
                options.AvailabiltyInShops || 
                options.ProductArticle ||
                name.EndsWith("...");

            if (needToGoToProductPage)
            {
                WebPage productPage = page.Site.GetPage(uri, TdPoiskRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var product = new WebMonitoringPosition(null, name, onlinePrice, retailPrice, uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//div[@id='paginator']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//div[@class='pagination-next']/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            bool areEqual = Equals(page.Uri.GetQueryParam("PAGEN_1"), nextPageUri.GetQueryParam("PAGEN_1"));
            if (areEqual)
                return null;

            return page.Site.GetPage(nextPageUri, TdPoiskRuWebPageType.Catalog, page.Path);
        }
    }
}