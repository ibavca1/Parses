using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.IonRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.IonRu
{
    internal class IonRuProductPageContentParser : HtmlPageContentParser, IIonRuWebPageContentParser
    {
        public IonRuWebPageType PageType
        {
            get { return IonRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode = content.GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");

            string article = productNode
                .GetSingleNode(@".//ul[@class='catalog_variant_prices']/li[@class='b']/b")
                .GetInnerText();
            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            decimal onlinePrice = 0; 
            decimal retailPrice = 0;

            bool isOutOfStock = productNode
                .HasNode(@".//div[contains(text(),'Этого товара нет в продаже')]");
            if (!isOutOfStock)
            {
                ParsePrices(productNode, out onlinePrice, out retailPrice);
            }

            var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri);
            if (retailPrice != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = ParseAvailabilityInShops(page, content, context).ToArray();
            }
            
            return WebPageContentParsingResult.FromPosition(product);
        }

        private static void ParsePrices(HtmlNode documentNode, out decimal onlinePrice, out decimal retailPrice)
        {
            HtmlNode priceNode = documentNode
                .GetSingleNode(@".//ul[@class='catalog_variant_prices']");
            decimal price = priceNode
                .SelectSingleNode(@".//li[contains(text(), 'Цена на сайте и в магазинах')]//span[@class='c']")
                .GetPrice();
            if (price != 0)
            {
                onlinePrice = price;
                retailPrice = price;
                return;
            }
            
            onlinePrice = priceNode
                .SelectSingleNode(@".//li[contains(text(), 'Цена в интернет-магазине')]//span[@class='c']")
                .GetPrice();
            retailPrice = priceNode
                .SelectSingleNode(@".//li[contains(text(), 'Цена в магазинах')]//span[@class='c']")
                .GetPrice();

            if (onlinePrice == 0 && retailPrice == 0)
                throw new InvalidWebPageMarkupException();
        }
    
        private static IEnumerable<WebProductAvailabilityInShop> 
            ParseAvailabilityInShops(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPage availabilityPage = page.Site.GetPage(page.Uri, IonRuWebPageType.AvailabilityInShops);
            WebPageRequest request = WebPageRequest.Create(availabilityPage);
            request.Cookies = content.Cookies;
            WebPageContent availabilityContent = page.Site.LoadPageContent(request, context);
            var parser = new IonRuShopsListPageContentParser();
            WebPageContentParsingResult result = parser.Parse(page, availabilityContent, context);
            return result.Shops.Select(s => new WebProductAvailabilityInShop(s.Name, s.Address, true));
        }
    }
}