using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.Nord24Ru;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    internal class Nord24RuCatalogPageContentParser : Nord24RuHtmlPageContentParser, INord24RuWebPageContentParser
    {
        public Nord24RuWebPageType PageType
        {
            get { return Nord24RuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(
            WebPage page, HtmlPageContent content)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[contains(@class,'justify')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//div[contains(@class,'itempr')]");

            var products = new List<WebMonitoringPosition>();
            foreach (HtmlNode productNode in productsNodes)
            {
                string name =
                    productNode.GetSingleNode(@".//a[@class='itempr_a']").GetAttributeText("title");
                Uri uri =
                    productNode.GetSingleNode(@".//a[@class='itempr_a']").GetUri(page);
                decimal price =
                    productNode.SelectSingleNode(@".//span[@class='price']").GetPrice();

                var product = new WebMonitoringPosition(null, name, price, uri);
                products.Add(product);
            }

            return new WebPageContentParsingResult { Positions = products };
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode paginationNode =
               content.SelectSingleNode(@"//div[@class='pageNav']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[@class='next']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            string nextPageNumber = nextPageNode.GetAttributeText("data-page");
            Uri nextPageUri = page.Uri.AddQueryParam("page", nextPageNumber);
            return page.Site.GetPage(nextPageUri, Nord24RuWebPageType.Catalog, page.Path);
        }
    }
}