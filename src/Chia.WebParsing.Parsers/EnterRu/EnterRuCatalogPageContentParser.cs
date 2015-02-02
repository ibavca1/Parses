using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EnterRu
{
    internal class EnterRuCatalogPageContentParser : EnterRuHtmlPageContentParser, IEnterRuWebPageContentParser
    {
        public EnterRuWebPageType PageType
        {
            get { return EnterRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPage[] categoriesPages = ParseCategories(page, content).ToArray();
            if (categoriesPages.Any())
                return new WebPageContentParsingResult {Pages = categoriesPages};

            WebPageContentParsingResult result = ParseProducts(page, content, context.Options);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlPageContent content)
        {
            HtmlNodeCollection categoriesNodes =
                content.SelectSingleNode(@"//ul[contains(@class,'bCatalogList')]")
                .With(x => x.SelectNodes(@".//a[@class='bCatalogList__eLink']"));
            if (categoriesNodes == null)
                return Enumerable.Empty<WebPage>();
            
            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                Uri uri = categoryNode.GetUri(page);
                string name = categoryNode
                    .GetSingleNode(".//span[@class='bCategoriesName']")
                    .GetInnerText();

                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, EnterRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return pages;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            bool isEmpty = content.HasNode(@"//li[@class='lstn_empty']");
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            HtmlNode productsList =
                content.GetSingleNode(@"//ul[contains(@class,'bListing')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//li[@class='bListingItem' and not(contains(@class, 'mBannerItem'))]");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                WebPageContentParsingResult product = ParseProductNode(page, productNode, options);
                result.Add(product);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProductNode(WebPage page, HtmlNode node, WebSiteParsingOptions options)
        {
            HtmlNode nameAndLinkNode =
                node.GetSingleNode(@".//p[@class='bSimplyDesc__eText']/a");

            string name = nameAndLinkNode.GetInnerText();
            Uri uri = nameAndLinkNode.GetUri(page);
            decimal price = 
                node.GetSingleNode(@".//span[contains(@class,'bPrice')]/strong[not(@class)]").GetPrice();

            bool needToGoToProductPage = options.ProductArticle;
            if (needToGoToProductPage)
            {
                WebPage productPage = page.Site.GetPage(uri, EnterRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var product = new WebMonitoringPosition(null, name, price, uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//ul[@class='bSortingList']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
              pagingNode.SelectSingleNode(@"li[contains(@class,'mActive')]/following-sibling::li/a[not(@href='#')]");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            WebPage nextPage = page.Site.GetPage(nextPageUri, EnterRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}