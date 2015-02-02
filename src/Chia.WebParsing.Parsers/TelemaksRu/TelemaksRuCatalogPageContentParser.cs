using System;
using System.Web;
using Chia.WebParsing.Companies.TelemaksRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal class TelemaksRuCatalogPageContentParser : TelemaksRuHtmlPageContentParser, ITelemaksRuWebPageContentParser
    {
        public TelemaksRuWebPageType PageType
        {
            get { return TelemaksRuWebPageType.Catalog; }
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

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            bool needToGoToPositionPage = options.AvailabiltyInShops;

            HtmlNode productsList =
                content.GetSingleNode(@"//div[contains(@class,'sort_view2') and @list='2']");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(".//div[contains(@class,'OneProductDescription')]");

            bool noProducts = productsNodes == null;
            if (noProducts)
                return WebPageContentParsingResult.Empty;

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                string article = 
                    productNode.GetAttributeText("id_good");
                string name = 
                    productNode.GetSingleNode(@".//div[@class='OneProductName']//a").GetInnerText();
                Uri uri =
                    productNode.GetSingleNode(@".//div[@class='OneProductName']//a").GetUri(page);
                decimal retailPrice = 
                    productNode.GetSingleNode(@".//div[@class='OneProductPrice_rz']/p/span").GetPrice();
                decimal onlinePrice = 0;

                bool isRetailOnly =
                    productNode.HasNode(".//p[text()='Данный товар доступен для продажи только в розничных магазинах']");
                if (!isRetailOnly)
                {
                    onlinePrice =
                        productNode.GetSingleNode(@".//div[@class='ProductInMarketPrice']/p[@class='priceim']").GetPrice();
                }

                if (needToGoToPositionPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, TelemaksRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                }
                else
                {
                    var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, uri);
                    result.Positions.Add(product);
                }
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode paginationNode =
                content.SelectSingleNode(@"//div[@class='SortPaginator']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[@title='Следующая страница']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            string href = nextPageNode.GetAttributeText("data_link") ?? nextPageNode.GetAttributeText("href");
            href = HttpUtility.UrlDecode(href);
            href = href.Replace("&javascript:void(0)", "");
            Uri nextPageUri = page.Site.MakeUri(href);

            bool isFirstPage = page.Uri.GetQueryParams()["p"] == null;
            bool areSameUri = nextPageUri.IsSame(page.Uri);
            if (!isFirstPage && areSameUri)
                return null;

            return page.Site.GetPage(nextPageUri, TelemaksRuWebPageType.Catalog, page.Path);
        }
    }
}