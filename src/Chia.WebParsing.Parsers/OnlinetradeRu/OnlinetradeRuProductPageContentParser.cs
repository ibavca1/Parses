using System;
using Chia.WebParsing.Companies.OnlinetradeRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal class OnlinetradeRuProductPageContentParser : OnlinetradeRuHtmlPageContentParser, IOnlinetradeRuWebPageContentParser
    {
        public OnlinetradeRuWebPageType PageType
        {
            get { return OnlinetradeRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");

            string article = productNode
                .GetSingleNode(@".//div[@class='codes']//span")
                .GetDigitsText();
            string name = productNode
                .GetSingleNode(@".//div[@itemprop='name']/h1")
                .GetInnerText();
            decimal price = productNode
                .GetSingleNode(@".//span[@itemprop='price']")
                .GetPrice();

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}