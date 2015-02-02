using Chia.WebParsing.Companies.TechnonetRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TechnonetRu
{
    internal abstract class TechnonetRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName =
                content.GetSingleNode(@"//p[@class='b-city-cur']/a").GetInnerText();
            string expectedCityName =
                 TechnonetRuCity.Get(page.Site.City).Name;

            bool areCitiesEqual =
                string.Equals(actualCityName, expectedCityName);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}