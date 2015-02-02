using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal class EldoradoRuCatalogPageContentParser : EldoradoRuHtmlPageContentParser, IEldoradoRuWebPageContentParser
    {
        public EldoradoRuWebPageType Type
        {
            get { return EldoradoRuWebPageType.Catalog; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPageContentParsingResult result = ParseProducts(page, content, context);

            WebPage nextPage = ParseNextPage(page, content);
            if (nextPage != null)
                result.Pages.Add(nextPage);

            return result;
        }

        private static WebPageContentParsingResult ParseProducts(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isEmpty =
                content.SelectSingleNode(
                    @"//*[@class='noItem' or text()='По заданным условиям не найдено ни одного товара или услуги.']") != null;
            if (isEmpty)
                return WebPageContentParsingResult.Empty;

            HtmlNode goodsList =
               content.GetSingleNode(@"//div[@class='goodsList']");
            HtmlNodeCollection goodsNodes =
               goodsList.GetNodes(@"//div[@class='item']");

            var result = new WebPageContentParsingResult();
            decimal prevPrice = 0;
            foreach (HtmlNode node in goodsNodes)
            {
                string name =
                    node.GetSingleNode(@".//div[@class='itemTitle']/a").GetInnerText();
                Uri uri =
                    node.GetSingleNode(@".//div[@class='itemTitle']/a").GetUri(page);
                bool isAction =
                    node.HasNode(@".//div[@class='action_shield']/a/img[not(contains(@src,'credit'))]");
                bool isOutOfStock =
                    node.HasNode(@".//div[contains(@class,'outOfStock')]") ||
                    node.DoesNotHaveNode(@".//span[@itemprop='price']");
                decimal onlinePrice = 0;
                //debug
                //***************************************************
                //***************************************************
                //end debug
                decimal retailPrice = 0;
                if (!isOutOfStock)
                {
                    onlinePrice = node
                        .GetSingleNode(@".//span[@itemprop='price']")
                        .GetPrice();
                    retailPrice = onlinePrice;
                    if (retailPrice <= prevPrice)
                    {
                        if (retailPrice < prevPrice)
                            return result;
                        if (!IsAvailability(page, uri, context))
                            return result;
                    }
                        
                    prevPrice = retailPrice;
                }

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                var product =
                    new WebMonitoringPosition(null, name, onlinePrice, retailPrice, uri)
                    {
                        IsAction = isAction
                    };

                bool needToGoToProductPage =
                    context.Options.ProductArticle || context.Options.AvailabiltyInShops;

                if (product.RetailPrice != 0 && needToGoToProductPage)
                {
                    WebPage productPage = page.Site.GetPage(product.Uri, EldoradoRuWebPageType.Product, page.Path);
                    result.Pages.Add(productPage);
                    continue;
                }

                result.Positions.Add(product);
            }

            return result;
        }

        private static bool IsAvailability(WebPage _page, Uri _uri, WebPageContentParsingContext context)
        {
            var pageUri = _uri;
            WebSite site = _page.Site;
            var page = _page;
            page.Uri = _uri;
            WebPageRequest request = WebPageRequest.Create(page);
            WebPageContent content = site.LoadPageContent(request, context);
            var _doc = new HtmlDocument();
            _doc.LoadHtml(content.ReadAsString());
            var _node = _doc.DocumentNode.SelectSingleNode(@"//meta[@itemtype='http://schema.org/ItemAvailability']");
            if (_node != null)
                return true;
            else
                return false;
        }

        private static WebPage ParseNextPage(WebPage page, HtmlPageContent document)
        {
            HtmlNode pagerNode =
                document.SelectSingleNode(@"//div[@class='pager']");
            bool noPager = pagerNode == null;
            if (noPager)
                return null;

            HtmlNode nextPageNode =
                pagerNode.SelectSingleNode(@".//a[@class='button buttonNext']");
            bool noNextPage = nextPageNode == null;
            if (noNextPage)
                return null;

            Uri nextPageUri = nextPageNode.GetUri(page);
            return page.Site.GetPage(nextPageUri, EldoradoRuWebPageType.Catalog, page.Path);
        }

        public virtual IDictionary<string, decimal> GetPrices(HtmlNodeCollection goodsNodes, WebSite site, WebPageContentParsingContext context)
        {
            var result = new Dictionary<string, decimal>();
            var bids = new Dictionary<string,string>();
            foreach (HtmlNode node in goodsNodes)
            {
                string name =
                    node
                        .GetSingleNode(@".//div[@class='itemTitle']/a")
                        .GetInnerText();

                HtmlNode bidNode = 
                    node.SelectSingleNode(@".//a[@data-bid]");
                if (bidNode != null)
                {
                    string bid = bidNode.GetAttributeText("data-bid");
                    bids.Add(bid, name);
                    continue;
                }

                decimal price = 
                    node
                        .SelectSingleNode(@"//div[@class='priceContainer']//p[@class='itemPrice']")
                        .GetPrice();
                result.Add(name, price);
            }

            if (!bids.Any()) 
                return result;

            WebPageRequest request = WebPageRequest.Create(EldoradoRuWebPageType.Prices);
            request.Data = bids.Keys;
            WebPageContent content = site.LoadPageContent(request, context);
            IDictionary<string, decimal> prices = ParseProductPrices(content);
            foreach (KeyValuePair<string, decimal> pair in prices)
            {
                string bid = pair.Key;
                result.Add(bids[bid], prices[bid]);
            }

            return result;
        }
    }
}