using Chia.WebParsing.Companies.TdPoiskRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal abstract class TdPoiskRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName =
                content.GetSingleNode(@"//div[@class='b-city-current__city']").GetInnerText();
            string expectedCityName =
                 TdPoiskRuCity.Get(page.Site.City).Name;

            bool areCitiesEqual =
                string.Equals(actualCityName, expectedCityName);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}