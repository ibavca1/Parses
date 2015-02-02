using Chia.WebParsing.Companies.RegardRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.RegardRu
{
    internal abstract class RegardRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = RegardRuCity.Get(page.Site.City).Name;
            string actualCityName = 
                content.GetSingleNode(@"//div[@id='chcity']/a").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}