using Chia.WebParsing.Companies.LogoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.LogoRu
{
    internal class LogoRuProductPageContentParser : LogoRuHtmlPageContentParser, ILogoRuWebPageContentParser
    {
        public LogoRuWebPageType PageType
        {
            get { return LogoRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@class='product']");

            string name = productNode
                .GetSingleNode(@"//div[@class='product']/h1")
                .GetInnerText();
            decimal price = 0;

            bool isAbsent = productNode.HasNode(@"//span[text()='нет в наличии']");
            if (!isAbsent)
            {
                price = productNode
                    .GetSingleNode(@"//span[@class='price']")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(null, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}