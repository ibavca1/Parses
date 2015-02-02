using Chia.WebParsing.Companies.TelemaksRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal abstract class TelemaksRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName =
                content.GetSingleNode(@"//a[contains(@class,'address-header')]").GetInnerText();
            string expectedCityName =
                TelemaksRuCity.Get(page.Site.City).Name;

            if (!string.Equals(actualCityName, expectedCityName))
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}