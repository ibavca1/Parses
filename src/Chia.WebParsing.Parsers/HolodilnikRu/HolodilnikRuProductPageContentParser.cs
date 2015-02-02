using System.Globalization;
using Chia.WebParsing.Companies.HolodilnikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    internal class HolodilnikRuProductPageContentParser : HolodilnikRuHtmlPageContentParser, IHolodilnikRuWebPageContentParser
    {
        public virtual HolodilnikRuWebPageType PageType
        {
            get { return HolodilnikRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode = content.Document.DocumentNode;
                //content.GetSingleNode(@"//tr[@itemtype='http://schema.org/Product']");

            string name = productNode
                .GetSingleNode(@"//h3[@itemprop='name']")
                .GetInnerText();
            string article = productNode
                .SelectSingleNode(@"//p[contains(text(),'Код товара:')]")
                .GetDigitsText();
            decimal price = 0;

            bool isPresent = productNode.HasNode(@".//div[@class='btn_buy']");
            if (isPresent)
            {
                price = productNode
                    .GetSingleNode(@"//*[@itemprop='price']")
                    .GetPrice(attribute: "content");
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}