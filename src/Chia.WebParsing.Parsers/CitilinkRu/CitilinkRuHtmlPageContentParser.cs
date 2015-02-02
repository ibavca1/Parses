using Chia.WebParsing.Companies.CitilinkRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    internal abstract class CitilinkRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = CitilinkRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//span[@id='city_select_link']").GetInnerText().Replace("—", "").Trim();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}