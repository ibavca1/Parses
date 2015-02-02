using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EurosetRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    internal class EurosetRuCatalogPageContentParser : EurosetRuHtmlPageContentParser, IEurosetRuWebPageContentParser
    {
        public EurosetRuWebPageType PageType
        {
            get { return EurosetRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//div[@class='phones']");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@"ul/li/a/..");

            var products = new List<WebMonitoringPosition>();
            foreach (HtmlNode productsNode in productsNodes)
            {
                string name = 
                    productsNode.GetSingleNode(@".//p[@class='product-name']").GetInnerText();
                decimal price = 0;
                Uri uri = 
                    productsNode.GetSingleNode(@".//a").GetUri(page);
                bool isOutOfStock =
                    productsNode.HasNode(@".//span[text()='НЕТ В НАЛИЧИИ']");
                if (!isOutOfStock)
                {
                    price =
                        productsNode.SelectSingleNode(@".//p[@class='product-cost']").GetPrice();
                }

                var product = new WebMonitoringPosition(null, name, price, uri);
                products.Add(product);
            }
            
            return new WebPageContentParsingResult {Positions = products};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
                document.SelectSingleNode(@"//div[@class='bx-nav-wrap']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@".//a[@class='bx-nav-next']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPage = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPage, EurosetRuWebPageType.Catalog, page.Path);
        }
    }
}