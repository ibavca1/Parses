using Chia.WebParsing.Companies.TehnosilaRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TehnosilaRu
{
    internal abstract class TehnosilaRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string extectedCityName =
                TehnosilaRuCity.Get(page.Site.City).Name;
            string actualCityName = 
                content.GetSingleNode(@"//span[@id='current-region']").GetInnerText();

            if (!string.Equals(actualCityName, extectedCityName))
                throw new InvalidWebCityException(extectedCityName, actualCityName);
        }
    }
}