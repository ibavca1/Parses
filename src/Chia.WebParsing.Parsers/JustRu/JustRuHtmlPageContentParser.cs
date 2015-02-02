using Chia.WebParsing.Companies.JustRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.JustRu
{
    internal abstract class JustRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = JustRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//a[@id='city-select-lnk']").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}