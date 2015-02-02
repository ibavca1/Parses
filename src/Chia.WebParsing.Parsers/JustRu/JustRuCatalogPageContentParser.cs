using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.JustRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.JustRu
{
    internal class JustRuCatalogPageContentParser : HtmlPageContentParser, IJustRuWebPageContentParser
    {
        public JustRuWebPageType PageType
        {
            get { return JustRuWebPageType.Catalog; }
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
                content.GetSingleNode(@"//ul[contains(@class,'catalog-list')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@"li[not(@class='also')]");

            var products = new List<WebMonitoringPosition>();
            foreach (HtmlNode productNode in productsNodes)
            {
                //bool isDiscounted = productNode.HasNode(@".//*[contains(@class,'discount')]");
                //if (isDiscounted)
                //    continue;

                string article = productNode
                    .GetSingleNode(@".//div[@class='marking']")
                    .GetDigitsText();
                string name = productNode
                    .GetSingleNode(@".//div[@class='description']/p/a")
                    .GetInnerText();
                decimal price = productNode
                    .GetSingleNode(@".//div[@class='price']/span")
                    .GetPrice();
                Uri uri = productNode
                    .GetSingleNode(@".//div[@class='description']/p/a")
                    .GetUri(page);
                var position = new WebMonitoringPosition(article, name, price, uri);
                products.Add(position);
            }

            return new WebPageContentParsingResult {Positions = products};
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagingNode =
               document.SelectSingleNode(@"//ul[@class='paging']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
                pagingNode.SelectSingleNode(@".//li[@class='next']/a[not(@style='visibility: hidden;')]");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri uri = nextPageNode.GetUri(page);
            return page.Site.GetPage(uri, JustRuWebPageType.Catalog, page.Path);
        }
    }
}