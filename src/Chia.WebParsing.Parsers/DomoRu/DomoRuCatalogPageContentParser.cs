using System;
using System.Text.RegularExpressions;
using System.Web;
using Chia.WebParsing.Companies.DomoRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal class DomoRuCatalogPageContentParser : DomoRuHtmlPageContentParser, IDomoRuWebPageContentParser
    {
        public virtual DomoRuWebPageType Type
        {
            get { return DomoRuWebPageType.Catalog; }
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

        protected static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebSiteParsingOptions options)
        {
            bool needToGoToProductPage = options.ProductArticle;

            HtmlNodeCollection productsNodes =
               content.GetNodes(@".//div[@class='product_info']");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productsNode in productsNodes)
            {
                string name = productsNode
                    .SelectSingleNode(@".//div[@class='product_name']/a")
                    .GetInnerText();
                Uri uri = productsNode
                    .GetSingleNode(@".//div[@class='product_name']/a")
                    .GetUri(page);
                decimal price = 0;

                bool isOutOfStock =
                    productsNode.HasNode(@".//a[@class='unavailable']");
                if (!isOutOfStock)
                {
                    price = productsNode
                        .GetSingleNode(@".//span[@itemprop='price']")
                        .GetPrice();
                }

                if (needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(uri, DomoRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                    continue;
                }
                var product = new WebMonitoringPosition(null, name, price, uri);
                result.Positions.Add(product);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode paginationNode =
                content.SelectSingleNode(@"//ul[@id='pages_list_id']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            string categoryId = ExtractCategoryId(page, content);

            Uri nextPageUri = page.Uri
                .AddQueryParam("categoryId", categoryId)
                .AddQueryParam("page", "2");

            WebPage nextPage = page.Site.GetPage(nextPageUri, DomoRuWebPageType.CatalogAjax, page.Path);
            nextPage.Cookies = page.Cookies;
            return nextPage;
        }

        private static string ExtractCategoryId(WebPage page, HtmlPageContent content)
        {
            string categoryId = page.Uri.GetQueryParam("categoryId");
            if (categoryId != null)
                return categoryId;

            string href = content
                .GetSingleNode(@".//a[@id='compare_button_enabled']")
                .GetAttributeText("href");
            href = HttpUtility.UrlDecode(href);
            Match match = Regex.Match(href, "categoryId: '([0-9]+)'");
            if (!match.Success)
                throw new InvalidWebPageMarkupException();

            return match.Groups[1].Value;
        }
    }
}