using System.Collections.Generic;
using Chia.WebParsing.Companies.TehnosilaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal class TehnosilaRuProductPageContentParser : TehnosilaRuHtmlPageContentParser, ITehnosilaRuWebPageContentParser
    {
        public TehnosilaRuWebPageType PageType
        {
            get { return TehnosilaRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@id='item-details']");
            string name = productNode
                .GetSingleNode(@"h1")
                .GetInnerText();
            string article = productNode
                .GetSingleNode(@".//span[@class='article']")
                .GetDigitsText();
            decimal price = 0;
            bool isPresent =
                productNode.DoesNotHaveNode(@".//*[@class='not-present' or @class='no-price']");

            if (isPresent)
            {
                price = content
                    .GetSingleNode(@".//*[@class='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);

            if (price != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = LoadAvailabilityInShopsHtml(page.Site, content, context);
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private WebProductAvailabilityInShop[] LoadAvailabilityInShopsHtml(
            WebSite site, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isNotAavailableInShops =
                content.HasNode(@"//span[@class='notavailable']");
            if (isNotAavailableInShops)
                return new WebProductAvailabilityInShop[0];

            string dataId = content
                .GetSingleNode(@".//a[@data-id]")
                .GetAttributeText(@"data-id");

            WebPageRequest request = 
                WebPageRequest.Create(TehnosilaRuWebPageType.ProductAvailabilityInShops);
            request.Cookies = content.Cookies;
            request.Data = dataId;
            WebPageContent shopsHtml = site.LoadPageContent(request, context);
            HtmlPageContent htmlContent = HtmlPageContent.Create(shopsHtml);
            return ParseAvailabilityInShopsHtml(htmlContent);
        }

        internal WebProductAvailabilityInShop[] ParseAvailabilityInShopsHtml(HtmlPageContent content)
        {
            HtmlNodeCollection shopsNodes =
                content.GetNodes(@"//div[@class='row']");
            bool noShops = shopsNodes == null;
            if (noShops)
                return new WebProductAvailabilityInShop[0];

            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string address = shopsNode
                    .GetSingleNode(@".//span[@class='title']")
                    .GetInnerText();
                bool isAvailable =
                    shopsNode.HasNode(@".//div[contains(text(),'в наличии')]");
                var shop = new WebProductAvailabilityInShop(address, isAvailable);
                shops.Add(shop);
            }

            return shops.ToArray();
        }
    }
}