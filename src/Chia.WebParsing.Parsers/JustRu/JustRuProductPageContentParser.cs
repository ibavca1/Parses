using Chia.WebParsing.Companies.JustRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.JustRu
{
    internal class JustRuProductPageContentParser : HtmlPageContentParser, IJustRuWebPageContentParser
    {
        public JustRuWebPageType PageType
        {
            get { return JustRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://data-vocabulary.org/Product']");

            string article = productNode
                .GetSingleNode(@".//span[@itemprop='identifier']")
                .GetInnerText();
            string name =productNode
                .GetSingleNode(@".//span[@itemprop='name']/h1")
                .GetInnerText();
            decimal price = 0;

            bool isAvailable = productNode.DoesNotHaveNode(@".//*[text()='Товара нет в наличии']");
            if (isAvailable)
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