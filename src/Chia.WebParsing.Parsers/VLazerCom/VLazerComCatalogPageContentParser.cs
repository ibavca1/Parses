using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.VLazerCom;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.VLazerCom
{
    internal class VLazerComCatalogPageContentParser : VLazerComHtmlPageContentParser, IVLazerComWebPageContentParser
    {
        public VLazerComWebPageType PageType
        {
            get { return VLazerComWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isEmpty =
                content.ReadAsString().Contains("К сожалению, в выбранной категории товары отсутствуют.");
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            WebPage[] categoriesPages = ParseCategories(page, content).ToArray();
            if (categoriesPages.Any())
                return new WebPageContentParsingResult { Pages = categoriesPages };

            WebPageContentParsingResult result = ParseProducts(page, content, context);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static IEnumerable<WebPage> ParseCategories(WebPage page, HtmlPageContent content)
        {
            HtmlNode categoriesList =
                content.SelectSingleNode(@"//div[@class='submenu']");
            if (categoriesList == null)
                return Enumerable.Empty<WebPage>();

            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//a");
            var pages = new List<WebPage>();
            foreach (HtmlNode subMenuItemsNode in categoriesNodes)
            {
                string name = subMenuItemsNode.GetInnerText();
                Uri uri = subMenuItemsNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, VLazerComWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return pages;
        }

        private WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//ul[@class='cellview grid']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//li[contains(@class,'cell')]");

            var result = new WebPageContentParsingResult();
            decimal priceOffset = GetPriceOffset(page, content, context);
            foreach (HtmlNode productNode in productsNodes)
            {
                string name = productNode
                    .GetSingleNode(@".//div[@class='name']/a")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//div[@class='name']/a")
                    .GetUri(page);
                string article = ExtractArticleFromUri(uri);
                decimal price = 0;

                price = productNode
                    .GetSingleNode(@".//ul[@class='price']/li")
                    .GetPrice();

                if (price != 0)
                    price -= priceOffset;

                var position = new WebMonitoringPosition(article, name, price, uri);
                result.Positions.Add(position);
            }

            return result;
        }

        private static string ExtractArticleFromUri(Uri uri)
        {
            string pageSegment = uri.Segments.Last();
            int dotIndex = pageSegment.LastIndexOf('.');
            string article = dotIndex != -1 ? pageSegment.Remove(dotIndex) : null;
            return article;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//ul[@class='btn_group']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[text()='Следующая']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, VLazerComWebPageType.Catalog, page.Path);
        }
    }
}