using Chia.WebParsing.Companies.EurosetRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.EurosetRu
{
    internal abstract class EurosetRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName =
                content.GetSingleNode(@"//li[@class='change-cities']/text()").GetInnerText();
            string expectedCityName =
                 EurosetRuCity.Get(page.Site.City).Name;

            bool areCitiesEqual =
                string.Equals(actualCityName, expectedCityName);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}