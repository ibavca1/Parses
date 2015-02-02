using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.MvideoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal class MvideoRuProductPageContentParser : MvideoRuHtmlPageContentParser, IMvideoRuWebPageContentParser
    {
        public MvideoRuWebPageType Type
        {
            get { return MvideoRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@class='content-top-section-layout']");

            bool isAction = productNode
                .HasNode(@".//div[@class='product-badge']");
            string name = productNode
                .GetSingleNode(@".//h1[@class='product-title']")
                .GetInnerText();
            string article = productNode
                .GetSingleNode(@".//div[@class='product-data-rating-code']/p")
                .GetDigitsText();
            decimal onlinePrice = 0;
            decimal retailPrice = 0;
            
            bool isRetailOnly = productNode
                .HasNode(@".//*[text()='Этот товар можно купить только в розничных магазинах']");
            bool outOfStock = productNode
                .HasNode(@".//*[text()='Товар временно отсутствует в продаже' or text()='Товар распродан']");
            bool isUnavailableForBuy = productNode
                .HasNode(@".//*[contains(text(),'В настоящее время товар недоступен для покупки')]");

            if (!outOfStock && !isUnavailableForBuy)
            {
                retailPrice = productNode
                    .GetSingleNode(@".//strong[@class='product-price-current']")
                    .GetPrice();
                if (!isRetailOnly)
                {
                    onlinePrice = retailPrice;
                }
            }
            WebMonitoringPosition position=null;
            //Для техносилы качаем розницу
            if ((context.Options.forClient == "techosila") && (isRetailOnly))
            {
                //onlinePrice = retailPrice;
                position = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri, "Розница") { IsAction = isAction };
            }
            else
                position = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri) { IsAction = isAction };
            
            if (retailPrice != 0 && context.Options.AvailabiltyInShops)
            {
                position.AvailabilityInShops = LoadAvailabilityInShops(page, context).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(position);
        }

        private static IEnumerable<WebProductAvailabilityInShop> 
            LoadAvailabilityInShops(WebPage page, WebPageContentParsingContext context)
        {
            WebPage availabilityPage = page.Site.GetPage(page.Uri, MvideoRuWebPageType.AvailabilityInShops, page.Path);
            WebPageRequest availabilityRequest = WebPageRequest.Create(availabilityPage);
            WebPageContent availabilityContent = page.Site.LoadPageContent(availabilityRequest, context);
            HtmlPageContent htmlContent = HtmlPageContent.Create(availabilityContent);
            return ParseAvailabilityInShops(htmlContent);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityInShops(HtmlPageContent content)
        {
            HtmlNode shopsList =
                content.SelectSingleNode(@"//div[@class='store-locator-list']");
            bool noShopsList = shopsList == null;
            if (noShopsList)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            HtmlNodeCollection shopsNodes =
                shopsList.SelectNodes(@".//li[@class='store-locator-list-item']");
            bool noShops = shopsNodes == null;
            if (noShops)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string address =
                    shopsNode.GetSingleNode(@".//div[@class='name']/p").GetInnerText();
                bool isAvailable =
                    shopsNode.DoesNotHaveNode(@".//i[@class='ico ico-stock-level-none']");

                var shop = new WebProductAvailabilityInShop(address, isAvailable);
                shops.Add(shop);
            }

            return shops;
        }
    }
}