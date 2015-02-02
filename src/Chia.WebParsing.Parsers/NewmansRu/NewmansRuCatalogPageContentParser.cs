using System;
using Chia.WebParsing.Companies.NewmansRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NewmansRu
{
    internal class NewmansRuCatalogPageContentParser : NewmansRuHtmlPageContentParser, INewmansRuWebPageContentParser
    {
        public NewmansRuWebPageType PageType
        {
            get { return NewmansRuWebPageType.Catalog; }
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
            bool needToGoToProductPage = options.ProductArticle;

            HtmlNode productsList =
                content.GetSingleNode(@"//div[@class='catalog-goods']");
            HtmlNodeCollection productNodes =
               productsList.GetNodes(@".//div[@idgoods]");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productNodes)
            {
                string name = productNode
                    .GetSingleNode(@".//div[@class='link']//a")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//div[@class='link']//a")
                    .GetUri(page);
                decimal price = 0;

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                bool isOutOfStock = productNode
                    .HasNode(@"//div[@class='price']/small[text()='Товара нет в продаже']");
                if (!isOutOfStock)
                {
                    price = productNode
                    .GetSingleNode(@".//div[@class='price']//big")
                    .GetPrice();
                }

                if (needToGoToProductPage)
                {
                    var productPage = page.Site.GetPage(uri, NewmansRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                }

                var product = new WebMonitoringPosition(null, name, price, uri);
                result.Positions.Add(product);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode nextPageNode = document.SelectSingleNode(@"//span[@id='page-next']/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, NewmansRuWebPageType.Catalog, page.Path);
        } 
    }
}