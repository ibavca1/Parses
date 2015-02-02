using System;
using Chia.WebParsing.Companies.OnlinetradeRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal class OnlinetradeRuCatalogPageContentParser : OnlinetradeRuHtmlPageContentParser, IOnlinetradeRuWebPageContentParser
    {
        public OnlinetradeRuWebPageType PageType
        {
            get { return OnlinetradeRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//table[@class='dealers']");
            HtmlNodeCollection productsNodes =
                productsList.SelectNodes(@".//tr[@class]");
            //bool noProducts = productsNodes == null;
            //if (noProducts)
            //    return WebPageContentParsingResult.Empty;

            var result = new WebPageContentParsingResult();
            foreach (HtmlNode productNode in productsNodes)
            {
                string article = productNode
                    .GetSingleNode(@".//td[1]/p")
                    .GetDigitsText();
                string name = productNode
                    .GetSingleNode(@".//a[@itemprop='name']")
                    .GetInnerText();
                Uri uri = productNode
                    .GetSingleNode(@".//a[@itemprop='name']")
                    .GetUri(page);
                decimal price = productNode
                    .GetSingleNode(@".//span[@itemprop='price']")
                    .GetPrice();

                var position = new WebMonitoringPosition(article, name, price, uri);
                result.Positions.Add(position);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode paginationNode =
                document.SelectSingleNode(@"//ul[@class='pagination']");
            bool noPagination = paginationNode == null;
            if (noPagination)
                return null;

            HtmlNode nextPageNode =
                paginationNode.SelectSingleNode(@".//a[contains(text(),'вперёд')]");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, OnlinetradeRuWebPageType.Catalog, page.Path);
        } 
    }
}