using Chia.WebParsing.Companies.TenIRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.TenIRu
{
    internal abstract class TenIRuHtmlPageContentParser : HtmlPageContentParser
    {
        //protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        //{
        //    base.ValidateContent(page, content, context);

        //    string expectedCityName = TenIRuCity.Get(page.Site.City).Name;
        //    string actualCityName =
        //        content.GetSingleNode(@"//a[@id='head_geo_select']").GetInnerText();

        //    bool areCitiesCorrect =
        //        string.Equals(expectedCityName, actualCityName);
        //    if (!areCitiesCorrect)
        //        throw new InvalidWebCityException(expectedCityName, actualCityName);
        //}
    }
}