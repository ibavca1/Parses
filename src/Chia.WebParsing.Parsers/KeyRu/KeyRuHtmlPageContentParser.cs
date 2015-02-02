using System.Net;
using Chia.WebParsing.Companies.KeyRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal abstract class KeyRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);
            CheckForDbError(content);

            string expectedCityName = KeyRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//span[@id='cityselect_city_label']").GetInnerText();

            bool areCitiesCorrect =
                     string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }

        private static void CheckForDbError(HtmlPageContent content)
        {
            bool isDbError = content.HasNode(@"//*[contains(text(),'Ошибка: невозможно подключиться к базе данных')]");
            if (isDbError)
                throw new WebException("Internal server error (503)");
        }
    }
}