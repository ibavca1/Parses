using Chia.WebParsing.Companies.CentrBtRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    internal abstract class CentrBtRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = CentrBtRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//select[@name='df_my_city']/option[@selected]").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}