using Chia.WebParsing.Companies.NewmansRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NewmansRu
{
    internal class NewmansRuProductPageContentParser : NewmansRuHtmlPageContentParser, INewmansRuWebPageContentParser
    {
        public NewmansRuWebPageType PageType
        {
            get { return NewmansRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = content
                .GetSingleNode(@"//div[@class='block-cart']/h1")
                .GetInnerText();
            string article = content
                .GetSingleNode(@"//div[@class='articul']")
                .GetInnerText();
            decimal price = 0;

            bool isOutOfStock = content.HasNode(@"//div[@class='notsale']");
            if (!isOutOfStock)
            {
                price = content
                    .GetSingleNode(@"//div[@class='real-price']")
                    .GetPrice();
            }

            if (string.IsNullOrWhiteSpace(name))
                return WebPageContentParsingResult.Empty;

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}