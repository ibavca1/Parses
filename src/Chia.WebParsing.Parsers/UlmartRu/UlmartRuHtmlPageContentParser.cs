using Chia.WebParsing.Companies.UlmartRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal abstract class UlmartRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName = 
                content.GetSingleNode(@"//span[contains(@class,'b-pseudolink black-lnk')]").GetInnerText();
            string expectedCityName =
                UlmartRuCity.Get(page.Site.City).Name;
            
            if (!string.Equals(expectedCityName, actualCityName))
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}