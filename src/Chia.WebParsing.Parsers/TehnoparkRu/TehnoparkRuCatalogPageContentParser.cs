using System;
using Chia.WebParsing.Companies.TehnoparkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    internal class TehnoparkRuCatalogPageContentParser : TehnoparkRuHtmlPageContentParser, ITehnoparkRuWebPageContentParser
    {
        public TehnoparkRuWebPageType PageType
        {
            get { return TehnoparkRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        internal static bool IsListPage(HtmlDocument document)
        {
            return document.DocumentNode.HasNode(@"//form[@id='filter-form']");
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection productsNodes = content
                .GetNodes(@"//li[@class='tp-wrap-product-line']");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                WebPageContentParsingResult product = ParseProduct(page, productNode);
                result.Add(product);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProduct(WebPage page, HtmlNode productNode)
        {
            string article = productNode
                .GetSingleNode(@".//dl[@class='article']/dd/span")
                .GetDigitsText();
            string name = productNode
                .GetSingleNode(@".//span[@itemprop='name']")
                .GetInnerText();
            Uri uri = productNode
                .GetSingleNode(@".//span[@itemprop='name']")
                .ParentNode
                .GetUri(page);
            decimal price = 0;

            bool isOutOfStock = productNode
                .HasNode(".//li[contains(text(),'Наличие: нет в наличии')]");
            if (!isOutOfStock)
            {
                price = productNode
                    .GetSingleNode(@".//strong[@class='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode nextButtonNode =
                document.SelectSingleNode(@"//a[@class='btn-next']");
            bool noNextPage = nextButtonNode == null;
            if (noNextPage)
                return null;

            Uri uri = nextButtonNode.GetUri(page);
            return page.Site.GetPage(uri, TehnoparkRuWebPageType.Catalog, page.Path);
        }
    }
}