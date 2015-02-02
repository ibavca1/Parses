using System;
using Chia.WebParsing.Companies.TehnosilaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal class TehnosilaRuCatalogPageContentParser : TehnosilaRuHtmlPageContentParser, ITehnosilaRuWebPageContentParser
    {
        public TehnosilaRuWebPageType PageType
        {
            get { return TehnosilaRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage parsedPage, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(parsedPage, content, context.Options);

            WebPage nextPage = ParseNextPage(parsedPage, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent document, WebSiteParsingOptions options)
        {
            HtmlNode catalogNode = document
                .GetSingleNode(@"//div[contains(@class,'cat_items')]");

            HtmlNodeCollection goodsNodes =
                catalogNode.SelectNodes(@".//div[contains(@class,'item-info-wrap')]");
            bool noGoods = goodsNodes == null;
            if (noGoods)
                return WebPageContentParsingResult.Empty;

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode goodNode in goodsNodes)
            {
                WebPageContentParsingResult productResult = ParseProductNode(page, goodNode, options);
                result.Add(productResult);
            }

            return result;
        }

        private static WebPageContentParsingResult ParseProductNode(
            WebPage page, HtmlNode productNode, WebSiteParsingOptions options)
        {
            HtmlNode nameAndUriNode = productNode
                .GetSingleNode(@".//div[@class='title']/a");

            string name = nameAndUriNode.GetInnerText();
            Uri uri = nameAndUriNode.GetUri(page);
            bool isAvailable =
                productNode.SelectSingleNode(@".//div[@class='not-in-stock']") == null;
            decimal price = 0;

            if (isAvailable)
            {
                price = productNode
                    .GetSingleNode(@".//div[@class='price']/span[@class='number' or @class='number-new']")
                    .GetPrice();
            }

            bool needToGoToProductPage =
                options.AvailabiltyInShops ||
                options.ProductArticle ||
                name.EndsWith("...");
            if (needToGoToProductPage)
            {
                WebPage productPage = page.Site.GetPage(uri, TehnosilaRuWebPageType.Product, page.Path);
                return WebPageContentParsingResult.FromPage(productPage);
            }

            var product = new WebMonitoringPosition(null, name, price, uri);
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//div[@class='pages']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@".//a[@class='next ajax']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, TehnosilaRuWebPageType.Catalog, page.Path);
        }
    }
}