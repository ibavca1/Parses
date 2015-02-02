using Chia.WebParsing.Companies.DomoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal class DomoRuProductPageContentParser : DomoRuHtmlPageContentParser, IDomoRuWebPageContentParser
    {
        public DomoRuWebPageType Type
        {
            get { return DomoRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNode productNode = content
                .GetSingleNode(@"//div[contains(@class,'container_product_details_image_information')]");

            string name = productNode
                .GetSingleNode(@".//span[@itemprop='name']")
                .GetInnerText();
            string article = productNode
                .GetSingleNode(@".//span[@class='sku']")
                .GetInnerText()
                .Replace("Артикул:", "");
            bool isOutOfStock =
                productNode.HasNode(@".//span[@class='sprite sprite-out-stock']");
            decimal price = 0;
            if (!isOutOfStock)
            {
                price = productNode
                    .GetSingleNode(@".//span[@itemprop='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}