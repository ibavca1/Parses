using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.TdPoiskRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Utils;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal class TdPoiskRuProductPageContentParser : TdPoiskRuHtmlPageContentParser, ITdPoiskRuWebPageContentParser
    {
        public TdPoiskRuWebPageType PageType
        {
            get { return TdPoiskRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");
            string article = 
                productNode.GetSingleNode(@"//span[@itemprop='sku']").GetDigitsText();
            string name =
                productNode.GetSingleNode(@"//h1[@itemprop='name']").GetInnerText();
            decimal retailPrice =
                productNode.SelectSingleNode(@"//div[@itemprop='price']").GetPrice();
            decimal onlinePrice = 0;

            //bool isRetailOnly =
            //    productNode.HasNode(".//div[contains(text(),'Только в розничных магазинах')]");
            bool isRetailOnly =
                productNode.GetSingleNode(".//div[@class='b-poduct-price-block']")
                    .InnerText.Contains("Только в розничных магазинах");
            if (!isRetailOnly)
            {
                onlinePrice = retailPrice;
            }

            var product = new WebMonitoringPosition(article, name, onlinePrice, retailPrice, page.Uri);
            
            if (retailPrice != 0 && context.Options.AvailabiltyInShops)
            {
                product.AvailabilityInShops = 
                    LoadAvailabilityInShops(page, content, context).ToArray();
            }

            return WebPageContentParsingResult.FromPosition(product);
        }

        private static IEnumerable<WebProductAvailabilityInShop> LoadAvailabilityInShops(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string pid = GetPid(content);
            WebPageRequest request = WebPageRequest.Create(TdPoiskRuWebPageType.AvailabilityInShops);
            request.Data = pid;
            WebPageContent shopsContent = page.Site.LoadPageContent(request, context);
            HtmlPageContent shopsHtmlContent = HtmlPageContent.Create(shopsContent);
            return ParseAvailabilityInShopsHtml(shopsHtmlContent);
        }

        private static IEnumerable<WebProductAvailabilityInShop> ParseAvailabilityInShopsHtml(HtmlPageContent content)
        {
            HtmlNodeCollection shopsNodes =
                content.SelectNodes(@"//a[@class='b-link']");
            bool noShops = shopsNodes == null;
            if (noShops)
                return Enumerable.Empty<WebProductAvailabilityInShop>();

            var shops = new List<WebProductAvailabilityInShop>();
            foreach (HtmlNode shopsNode in shopsNodes)
            {
                string name = shopsNode.GetInnerText();
                var shop = new WebProductAvailabilityInShop(name, true);
                shops.Add(shop);
            }

            return shops;
        }

        private static string GetPid(HtmlPageContent content)
        {
            HtmlNode compareNode =
                content.GetSingleNode(@"//a[@title='Сравнить/В списке сравнения']");
            return compareNode.GetAttributeText(@"class").RemoveNonDigitChars();
        }
    }
}