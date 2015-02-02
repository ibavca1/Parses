using Chia.WebParsing.Companies.VLazerCom;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.VLazerCom
{
    internal class VLazerComProductPageContentParser : VLazerComHtmlPageContentParser, IVLazerComWebPageContentParser
    {
        public VLazerComWebPageType PageType
        {
            get { return VLazerComWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string article = content
                .GetSingleNode(@"//span[contains(text(),'Артикул:')]")
                .GetDigitsText();
            string name = content
                .GetSingleNode(@"//div[contains(@class,'basewidth')]/h1")
                .GetInnerText();
            decimal price = content
                .GetSingleNode(@"//div[@class='price']//li[contains(@class,'sum')]")
                .GetPrice();

            if (price != 0)
            {
                decimal priceOffset = GetPriceOffset(page, content, context);
                price -= priceOffset;
            }
                
            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}