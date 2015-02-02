using Chia.WebParsing.Companies.RegardRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.RegardRu
{
    internal class RegardRuProductPageContentParser : RegardRuHtmlPageContentParser, IRegardRuWebPageContentParser
    {
        public RegardRuWebPageType PageType
        {
            get { return RegardRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = content
                .GetSingleNode(@"//div[@id='goods_head']")
                .GetInnerText();
            string article = content
                .GetSingleNode(@"//div[@class='goods_id']")
                .GetDigitsText();
            string characteristics = content
                .GetSingleNode(@"//div[@class='block-text']/span")
                .GetInnerText();
            decimal price = 0;

            bool isAvailable = content.HasNode(@"//a[@id='goods_sam']");
            if (isAvailable)
            {
                price = content
                    .GetSingleNode(@"//span[@class='price lot']/span")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri)
            {
                Characteristics = characteristics
            };

            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}