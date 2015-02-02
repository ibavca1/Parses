using Chia.WebParsing.Companies.OnlinetradeRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal abstract class OnlinetradeRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = OnlinetradeRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//div[@class='link caption']//a[@class='cchgl_url']").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }
    }
}