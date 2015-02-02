using System;
using Chia.WebParsing.Companies.OldiRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OldiRu
{
    internal class OldiRuCatalogPageContentParser : OldiRuHtmlPageContentParser, IOldiRuWebPageContentParser
    {
        public OldiRuWebPageType PageType
        {
            get { return OldiRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isEmpty = content
                .HasNode(@"//p[contains(text(),'Товаров по заданным критериям фильтра не найдено')]");
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            bool needToGoToProductPage =
                options.AvailabiltyInShops ||
                options.PriceTypes.HasFlag(WebPriceType.Retail);

            HtmlNode productsList =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/ItemList']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//div[@itemtype='http://schema.org/Product']");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                string article = productNode
                    .GetSingleNode(@".//div[@class='itemdescription']/p/strong")
                    .GetDigitsText();
                string name = productNode
                    .GetSingleNode(@".//div[@itemprop='name']/a")
                    .GetInnerText();
                string characteristics = productNode
                    .SelectSingleNode(".//div[contains(@class,'itemnamedesrc')]")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//div[@itemprop='name']/a")
                    .GetUri(page);
                decimal price = 0;

                bool isAvailable = productNode
                    .HasNode(@".//div[@itemtype='http://schema.org/Offer']");
                if (isAvailable)
                {
                    price = productNode
                        .GetSingleNode(@".//meta[@itemprop='price']")
                        .GetPrice("content");
                }

                if (needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, OldiRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                }
                else
                {
                    var product = new WebMonitoringPosition(article, name, price, uri)
                                      {Characteristics = characteristics};
                    result.Positions.Add(product);
                }
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//div[contains(@class,'pagerules')]");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@"ul/li/a[not(@class)]");
            bool noMorePages = nextPageNode == null;
            if (noMorePages)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, OldiRuWebPageType.Catalog, page.Path);
        } 
    }
}