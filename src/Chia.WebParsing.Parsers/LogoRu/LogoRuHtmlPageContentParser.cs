using Chia.WebParsing.Companies.LogoRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.LogoRu
{
    internal abstract class LogoRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = LogoRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//div[@id='choose_city']/span").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}