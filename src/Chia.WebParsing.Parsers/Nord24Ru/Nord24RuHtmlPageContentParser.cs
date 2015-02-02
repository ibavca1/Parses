using Chia.WebParsing.Companies.Nord24Ru;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.Nord24Ru
{
    internal abstract class Nord24RuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName =
                content.GetSingleNode(@"//p[@id='shops_list']").GetInnerText();
            string expectedCityName =
                 Nord24RuCity.Get(page.Site.City).Name;

            bool areCitiesEqual =
                string.Equals(actualCityName, expectedCityName);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}