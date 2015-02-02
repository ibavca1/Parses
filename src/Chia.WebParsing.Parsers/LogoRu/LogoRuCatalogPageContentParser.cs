using System;
using Chia.WebParsing.Companies.LogoRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.LogoRu
{
    internal class LogoRuCatalogPageContentParser : LogoRuHtmlPageContentParser, ILogoRuWebPageContentParser
    {
        public LogoRuWebPageType PageType
        {
            get { return LogoRuWebPageType.Catalog; }
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

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content)
        {
            HtmlNode productsList =
                content.GetSingleNode(@"//div[contains(@class,'justify')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//div[contains(@class,'itempr')]");

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                string name = productNode
                    .GetSingleNode(@".//a[@class='name']")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//a[@class='name']")
                    .GetUri(page);
                decimal price = 0;

                bool isAvailable = productNode
                    .HasNode(@".//div[@class='priceBlock']");
                if (isAvailable)
                {
                    price = productNode
                        .GetSingleNode(@".//span[@class='price']")
                        .GetPrice();
                }

                var product = new WebMonitoringPosition(null, name, price, uri);
                result.Positions.Add(product);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//div[@class='pageNav']");
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
            return page.Site.GetPage(nextPageUri, LogoRuWebPageType.Catalog, page.Path);
        } 
    }
}