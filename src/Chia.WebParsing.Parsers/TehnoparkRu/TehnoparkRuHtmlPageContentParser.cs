using Chia.WebParsing.Companies.TehnoparkRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TehnoparkRu
{
    internal abstract class TehnoparkRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = 
                TehnoparkRuCity.Get(page.Site.City).Name;
            string actualCityName =  
                content
                    .GetSingleNode(@"//a[@class='tp-region-modal-show region open-popup']")
                    .GetInnerText();

            bool areCitiesEqual = 
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}