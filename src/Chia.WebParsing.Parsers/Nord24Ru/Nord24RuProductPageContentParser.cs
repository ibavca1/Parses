using Chia.WebParsing.Companies.Nord24Ru;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    internal class Nord24RuProductPageContentParser : Nord24RuHtmlPageContentParser, INord24RuWebPageContentParser
    {
        public Nord24RuWebPageType PageType
        {
            get { return Nord24RuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            //HtmlNode productNode =
            //    content.GetSingleNode(@"//div[@class='product']");

            HtmlNode productNode =
                content.Document.DocumentNode;

            string name =
                productNode.GetSingleNode(@".//div[contains(@class,'nameBlock')]/h1").GetInnerText();
            decimal price =
                productNode.GetSingleNode(@".//div[@class='price']").GetPrice();

            var product = new WebMonitoringPosition(null, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}