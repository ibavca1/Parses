using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnoshokRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal class TehnoshokRuCatalogPageContentParser : TehnoshokRuHtmlPageContentParser, ITehnoshokRuWebPageContentParser
    {
        public TehnoshokRuWebPageType PageType
        {
            get { return TehnoshokRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isEmpty = IsEmpty(content);
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        internal static bool IsListPage(HtmlPageContent content)
        {
            return content.HasNode(@"//section[@class='param box']");
        }

        private static bool IsEmpty(HtmlPageContent content)
        {
            return content.DoesNotHaveNode(@"//div[@class='product-info']");
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[@class='content']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@"//div[@class='product-info']");

            var pages = new List<WebPage>();
            foreach (HtmlNode productNode in productsNodes)
            {
                Uri uri = productNode
                    .GetSingleNode(@".//a[@class='grey title']").GetUri(page);

                WebPage productPage = page.Site.GetPage(uri, TehnoshokRuWebPageType.Product, page.Path);
                pages.Add(productPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode nextPageNode =
                content.SelectSingleNode(@"//li[@class='next-off']/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri uri = nextPageNode.GetUri(page);
            WebPage nextPage = page.Site.GetPage(uri, TehnoshokRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}