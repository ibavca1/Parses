using Chia.WebParsing.Companies.CorpCentreRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal abstract class CorpCentreRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = CorpCentreRuCity.Get(page.Site.City).Name;
            string actualCityName = 
                content.GetSingleNode(@"//div[@class='store-item']/a[@class='popup-open']").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}