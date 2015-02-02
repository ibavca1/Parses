using System;
using Chia.WebParsing.Companies.DnsShopRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal class DnsShopRuCatalogPageContentParser : DnsShopRuHtmlPageContentParser, IDnsShopRuWebPageContentParser
    {
        public DnsShopRuWebPageType Type
        {
            get { return DnsShopRuWebPageType.Catalog; }
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
            var result = new WebPageContentParsingResult();
            HtmlNode productsList =
                content.GetSingleNode(@"//table[contains(@class,'catalog_view_list')]");
            HtmlNodeCollection productsNodes =
                productsList.GetNodes(@".//tr[@data-price_item_id]");

            foreach (HtmlNode productsNode in productsNodes)
            {
                string article = productsNode
                    .GetSingleNode(@".//td[@class='code']")
                    .GetInnerText();
                string name = productsNode
                    .GetSingleNode(@".//td[@class='title']/a[not(@class='image')]")
                    .GetInnerText();
                Uri uri = productsNode
                    .GetSingleNode(@".//td[@class='title']/a[not(@class='image')]")
                    .GetUri(page);
                string characteristics = productsNode
                    .SelectSingleNode(@".//td[@class='title']/p[@class='spec']")
                    .GetInnerText();
                bool isOutOfStock =
                    productsNode.HasNode(@".//td[@class='shop_avail' and text()='нет в наличии']");
                decimal price = 0;

                if (!isOutOfStock)
                {
                    HtmlNode priceNode =
                        productsNode.SelectSingleNode(@".//td[@class='price']/div[@class='new']") ??
                        productsNode.SelectSingleNode(@".//td[@class='price']");
                    price = priceNode.WithValidate().GetPrice();
                }

                if (price != 0 && options.AvailabiltyInShops)
                {
                    WebPage productPage = page.Site.GetPage(uri, DnsShopRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(characteristics))
                {
                    name = string.Format("{0} $$ {1}", name, characteristics);
                }

                var product =
                    new WebMonitoringPosition(article, name, price, price, uri)
                {
                    Characteristics = characteristics
                };

                result.Positions.Add(product);
            }

            return result;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent content)
        {
            HtmlNode pagingNode =
                content.SelectSingleNode(@"//ul[@class='pager']");
            bool noPaging = pagingNode == null;
            if (noPaging)
                return null;

            HtmlNode nextPageNode =
              pagingNode.SelectSingleNode(@"li[@class='sel']/following-sibling::li/a");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            WebPage nextPage = page.Site.GetPage(nextPageUri, DnsShopRuWebPageType.Catalog, page.Path);
            return nextPage;
        }
    }
}