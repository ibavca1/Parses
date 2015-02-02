using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.EldoradoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal class EldoradoRuProductPageContentParser : EldoradoRuHtmlPageContentParser, IEldoradoRuWebPageContentParser
    {
        public EldoradoRuWebPageType Type
        {
            get { return EldoradoRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@id='showcase']");
            //var tt = productNode.SelectNodes(@".//div[@class='bottomBlockContentRight ']/meta");
            //bool TestEldo = productNode.HasNode(@".//meta[@itemtype='http://schema.org/ItemAvailability']");
            //bool isOutOfStock = 
                //!productNode.HasNode(@".//meta[@itemtype='http://schema.org/ItemAvailability']");
            bool isOutOfStock = false;
                //productNode.HasNode(@".//div[contains(@class,'outOfStock')]") ||
                //productNode.DoesNotHaveNode(@".//span[@itemprop='price']");
            bool isAction =
                productNode.HasNode(@"//div[@class='action_shield']/a[not(contains(@href,'credit'))]");
            string name =
                productNode.GetSingleNode(@"//h1[@itemprop='name']").GetInnerText();
            string article =
                productNode.SelectSingleNode(@"//div[@class='article']").GetDigitsText();
            decimal onlinePrice = 0;
            decimal retailPrice = 0;

            if (!isOutOfStock)
            {
                onlinePrice =
                    //LoadPrice(content, page.Site, context);

                    productNode
                        .GetSingleNode(@"//span[@itemprop='price']")
                        .GetPrice();
                retailPrice = onlinePrice;
            }

            var position = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri) {IsAction = isAction};

            if (retailPrice != 0 && context.Options.AvailabiltyInShops)
            {
                position.AvailabilityInShops = 
                    LoadAvailabilityInRetailShops(content, page.Site, context).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(position);
        }

        internal virtual decimal LoadPrice(HtmlPageContent content, WebSite site, WebPageContentParsingContext context)
        {
            string bid = content
                .GetSingleNode(@"//a[@data-bid]")
                .GetAttributeText(@"data-bid");

            var request = WebPageRequest.Create(EldoradoRuWebPageType.Prices);
            request.Data = new[] {bid};
            WebPageContent priceContent = site.LoadPageContent(request, context);
            IDictionary<string, decimal> prices = ParseProductPrices(priceContent);
            return prices[bid];
        }

        internal virtual WebProductAvailabilityInShop[] LoadAvailabilityInRetailShops(
            HtmlPageContent content, WebSite site, WebPageContentParsingContext context)
        {
            string article = content
                .SelectSingleNode(@"//div[@class='article']")
                .GetDigitsText();
            WebPageRequest request = WebPageRequest.Create(EldoradoRuWebPageType.AvailabilityInShops);
            request.Cookies = content.Cookies;
            request.Data = article;

            for (int i = 0; i < 3; ++i)
            {
                WebPageContent shopsContent = site.LoadPageContent(request, context);
                HtmlPageContent html = HtmlPageContent.Create(shopsContent);
                IList<WebProductAvailabilityInShop> shops;
                if (TryParseAvailabilityInShopsHtml(html, out shops))
                    return shops.ToArray();
            }

            return new WebProductAvailabilityInShop[0];
        }

        internal bool TryParseAvailabilityInShopsHtml(HtmlPageContent content, out IList<WebProductAvailabilityInShop> shops)
        {
            shops = new List<WebProductAvailabilityInShop>();

            string text = content.ReadAsString();
            bool isFailed =
                string.IsNullOrWhiteSpace(text) ||
               text.Contains("»з-за технических проблем, оформление заказа с возможностью получени€ в магазине, в данный момент невозможно.");
            if (isFailed)
                return false;

            bool unavailableInShops =
                text.Contains("  сожалению, во всех магазинах вашего города товар отсутствует.");
            if (unavailableInShops)
                return true;

            HtmlNode shopsList =
                content.GetSingleNode(@"//ul[@class='containerSelfDelivPopUp']/li[last()]");
            HtmlNodeCollection shopsNodes =
                shopsList.GetNodes(@".//ul/li//tr");
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string address =
                    shopsNode.GetSingleNode(@".//div[@class='shop']/a").GetInnerText();
                bool isAvailable =
                    shopsNode.HasNode(@".//div[@class='self_how_much']/b[text()='—егодн€' or text()='«автра']");

                var shop = new WebProductAvailabilityInShop(address, address, isAvailable);
                shops.Add(shop);
            }

            return true;
        }
    }
}