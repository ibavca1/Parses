using Chia.WebParsing.Companies.ElectrovenikRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    internal abstract class ElectrovenikRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = ElectrovenikRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//a[@id='city_link']").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}