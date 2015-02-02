using System.Net;
using System.Web;
using Chia.WebParsing.Companies.SvyaznoyRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    internal abstract class SvyaznoyRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            SvyaznoyRuCity city = SvyaznoyRuCity.Get(page.Site.City);
            string expectedCityName = city.Name;
            string actualCityName = GetCityName(content);

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }

        private static string GetCityName(HtmlPageContent content)
        {
            Cookie cityCookie = content.Cookies["SHOWCITY"];
            if (cityCookie == null)
                return null;

            string value = cityCookie.Value;
            string name = HttpUtility.UrlDecode(value);
            return name;
        }
    }
}