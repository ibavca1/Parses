using Chia.WebParsing.Companies.TehnoshokRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TehnoshokRu
{
    internal abstract class TehnoshokRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = TehnoshokRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//div[@id='city-down']//span[@class='dashed']").GetInnerText();

            bool areCitiesEqual = string.Equals(expectedCityName, actualCityName);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}