using System;
using Chia.WebParsing.Companies.PultRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.PultRu
{
    internal class PultRuCatalogPageContentParser : PultRuHtmlPageContentParser, IPultRuWebPageContentParser
    {
        public PultRuWebPageType PageType
        {
            get { return PultRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//div[@id='product_table']");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(@".//div[contains(@class,'item ')]");
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
            string name =
                productNode
                    .GetSingleNode(@".//div[@class='tit']/a[@title]")
                    .GetInnerText();
            Uri uri =
                productNode
                    .GetSingleNode(@".//div[@class='tit']/a[@title]")
                    .GetUri(page);
            decimal price =
                productNode
                    .GetSingleNode(@".//div[@class='price']")
                    .GetPrice();

            bool needToGoToProductPage = options.ProductArticle;
            if (needToGoToProductPage)
            {
                WebPage productPage = page.Site.GetPage(uri, PultRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var product = new WebMonitoringPosition(null, name, price, uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode pagingNode =
               content.SelectSingleNode(@"//div[@class='pagingCenter']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
              pagingNode.SelectSingleNode(@"a[@class='selected']/following-sibling::a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            WebPage nextPage = page.Site.GetPage(nextPageUri, PultRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}