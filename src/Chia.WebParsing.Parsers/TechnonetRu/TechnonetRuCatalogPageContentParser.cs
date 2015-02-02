using System;
using Chia.WebParsing.Companies.TechnonetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal class TechnonetRuCatalogPageContentParser : TechnonetRuHtmlPageContentParser, ITechnonetRuWebPageContentParser
    {
        public TechnonetRuWebPageType PageType
        {
            get { return TechnonetRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//div[contains(@class,'b-catalog')]");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(@".//div[@class='b-catalog-item']");

            bool noProducts = productsNodes == null;
            if (noProducts)
                return WebPageContentParsingResult.Empty;

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
            string characteristics =
                productNode.GetSingleNode(@".//p[@class='b-catalog-name']/b").GetInnerText();
            string name =
                productNode.GetSingleNode(@".//p[@class='b-catalog-name']/a").GetInnerText();
            Uri uri =
                productNode.GetSingleNode(@".//p[@class='b-catalog-name']/a").GetUri(page);
            decimal price = 
                productNode.GetSingleNode(@".//div[@class='b-catalog-price']//span[@class='b-catalog-prow3']").GetPrice();

            bool needToGoToProductPage =
                options.AvailabiltyInShops || 
                options.ProductArticle ||
                name.EndsWith("...");

            if (needToGoToProductPage)
            {
                WebPage productPage = page.Site.GetPage(uri, TechnonetRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var product = new WebMonitoringPosition(null, name, price, uri)
                              {
                                  Characteristics = characteristics
                              };
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//nav[@class='b-catalog-control-pager']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[text()='След.']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, TechnonetRuWebPageType.Catalog, page.Path);
        }
    }
}