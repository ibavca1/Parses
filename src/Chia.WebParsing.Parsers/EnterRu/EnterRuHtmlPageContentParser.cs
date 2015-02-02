using Chia.WebParsing.Companies.EnterRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.EnterRu
{
    internal abstract class EnterRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string actualCityName = 
                content.GetSingleNode(@"//a[@data-region-id or contains(@class,'hdcontacts_lk')]").GetInnerText();
            string expectedCityName =
                EnterRuCity.Get(page.Site.City).Name;

            if (!string.Equals(actualCityName, expectedCityName))
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}