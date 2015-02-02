using Chia.WebParsing.Companies.HolodilnikRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    internal abstract class HolodilnikRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = HolodilnikRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//div[contains(@class,'your_reg')]/span").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}