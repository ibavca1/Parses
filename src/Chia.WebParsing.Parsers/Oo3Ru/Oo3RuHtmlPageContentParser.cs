using Chia.WebParsing.Companies.Oo3Ru;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Oo3Ru
{
    internal abstract class Oo3RuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = Oo3RuCity.Get(page.Site.City).Name;
            string actualCityName =
                //content.GetSingleNode(@"//a[@id='top_city']").GetInnerText();
                content.GetSingleNode(@"//a[@id='btnChangeTown']").GetInnerText();
            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}