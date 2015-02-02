using Chia.WebParsing.Companies.Oo3Ru;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Oo3Ru
{
    internal class Oo3RuProductPageContentParser : Oo3RuHtmlPageContentParser, IOo3RuWebPageContentParser
    {
        public Oo3RuWebPageType PageType
        {
            get { return Oo3RuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");

            bool isOutOfStock = productNode
                .HasNode(@".//li[@class='out_stock_message']");

            string name = productNode
                .GetSingleNode(@".//h1[@itemprop='name']")
                .GetInnerText();
            string article = productNode
                .SelectSingleNode(@".//li[contains(text(),'Артикул:')]")
                .GetDigitsText();
            decimal price = 0;
            if (!isOutOfStock)
            {
                price = productNode
                    .SelectSingleNode(@".//div[@itemprop='price']/text()")
                    .GetPrice();
            }

            var position = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(position);
        }
    }
}