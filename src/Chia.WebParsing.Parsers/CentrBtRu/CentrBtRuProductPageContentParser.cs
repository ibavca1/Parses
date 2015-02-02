using Chia.WebParsing.Companies.CentrBtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    internal class CentrBtRuProductPageContentParser : CentrBtRuMainPageContentParser
    {
        public override CentrBtRuWebPageType PageType
        {
            get { return CentrBtRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string article =
                content.SelectSingleNode(@"//*[contains(text(), 'Код товара')]").GetDigitsText();
            string name = 
                content.GetSingleNode(@"//span[@class='catalog_title']").GetInnerText();
            decimal price = 0;
            bool isAvailable = 
                content.DoesNotHaveNode(@"//span[@class='attr_item' and text()='нет в наличии']");
            //if (isAvailable)
            {
                price = content.GetSingleNode(@"//span[@class='cena']").GetPrice();
            }

            var product = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(product);
        }
    }
}