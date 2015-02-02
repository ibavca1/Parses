using Chia.WebParsing.Companies.EurosetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    internal class EurosetRuProductPageContentParser : EurosetRuHtmlPageContentParser, IEurosetRuWebPageContentParser
    {
        public EurosetRuWebPageType PageType
        {
            get { return EurosetRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = 
                content.GetSingleNode(@"//div[@class='header']/h1").GetInnerText();
            decimal price = 0;
            bool isOutOfStock =
                content.HasNode(@"//div[@class='thereisno']");
            if (!isOutOfStock)
            {
                price =
                    content.GetSingleNode(@"//span[@itemprop='price']").GetPrice();
            }

            var product = new WebMonitoringPosition(null, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}