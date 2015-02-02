using Chia.WebParsing.Companies.TechportRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TechportRu
{
    internal abstract class TechportRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            if (content.IsJavaScriptProcessed)
            {
                string expectedCityName = TechportRuCity.Get(page.Site.City).Name;
                string actualCityName =
                    content.GetSingleNode(@"//a[contains(@class,'rgn_selection_b')]/text()").GetInnerText();

                bool areCitiesCorrect =
                    string.Equals(expectedCityName, actualCityName);
                if (!areCitiesCorrect)
                    throw new InvalidWebCityException(expectedCityName, actualCityName);
            }
        }

        protected static decimal GetPrice(TechportRuCity city, HtmlNode productNode)
        {
            string outOfStockXPath =
                string.Format(
                    @".//div[contains(@id,'rgnArea') and contains(@id, '{0}') and contains(@class,'item_is_not_available')]", city.CookieValue);
            bool isOutOfStock = productNode.HasNode(outOfStockXPath);
            if (isOutOfStock)
                return 0;

            string priceXPath =
                string.Format(@".//div[contains(@id,'rgnArea') and contains(@id, '{0}')]/span[@class='price-price']", city.CookieValue);
            decimal price = productNode
                .GetSingleNode(priceXPath)
                .GetPrice();
            return price;
        }
    }
}