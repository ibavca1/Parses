using System;
using Chia.WebParsing.Companies.NotikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NotikRu
{
    internal class NotikRuCatalogPageContentParser : NotikRuHtmlPageContentParser, INotikRuWebPageContentParser
    {
        public NotikRuWebPageType PageType
        {
            get { return NotikRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//div[@class='inset']");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(@".//div[contains(@class,'notelist')]");

            bool noProducts = productsNodes == null;
            if (noProducts)
                return WebPageContentParsingResult.Empty;

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode positionsNode in productsNodes)
            {
                const string article = null;
                string name = positionsNode
                    .SelectSingleNode(@".//a")
                    .GetInnerText();
                Uri uri = positionsNode
                    .SelectSingleNode(@".//a")
                    .GetUri(page);
                decimal price = positionsNode
                    .GetSingleNode(@".//div[@class='price']")
                    .GetPrice();

                if (needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, NotikRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                }
                else
                {
                    var position = new WebMonitoringPosition(article, name, price, uri);
                    result.Positions.Add(position);
                }
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
               document.SelectSingleNode(@"//div[@class='paginator']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[contains(@class,'active')]/following-sibling::a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, NotikRuWebPageType.Catalog, page.Path);
        } 
    }
}