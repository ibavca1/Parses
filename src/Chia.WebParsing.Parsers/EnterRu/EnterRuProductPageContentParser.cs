using System.Linq;
using Chia.WebParsing.Companies.EnterRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EnterRu
{
    internal class EnterRuProductPageContentParser : EnterRuHtmlPageContentParser, IEnterRuWebPageContentParser
    {
        public EnterRuWebPageType PageType
        {
            get { return EnterRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode productNode =
                content.GetSingleNode(@"//div[@itemtype='http://schema.org/Product']");
            string article = 
                productNode.GetSingleNode(@".//span[@class='bPageHead__eArticle']")
                    .GetInnerText().Replace("Артикул: ", "");
            decimal price =
                productNode.GetSingleNode(@".//strong[@itemprop='price']").GetPrice();
            string model =
                productNode.GetSingleNode(@".//h1[@itemprop='name']").GetInnerText();
            string tradeGroup =
                productNode.GetSingleNode(@".//div[@class='bPageHead']/div[@class='bPageHead__eSubtitle']").GetInnerText();
            string name =
                string.Join(" ", new[] {tradeGroup, model}.Where(x => !string.IsNullOrEmpty(x))).Trim();

            if (string.IsNullOrEmpty(name))
            {
                name =
                    productNode.GetSingleNode(@".//div[@class='bPageHead__eSubtitle']").GetInnerText();
            }

            var position = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(position);
        }
    }
}