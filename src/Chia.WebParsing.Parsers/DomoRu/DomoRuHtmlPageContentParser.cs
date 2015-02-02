using Chia.WebParsing.Companies.DomoRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal abstract class DomoRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            HtmlNode cityNode =
                content.GetSingleNode(@"//div[@id='tsc_header_stores']/a");
            string cityName = cityNode.GetInnerText();
            DomoRuCity town = DomoRuCity.Get(page.Site.City);

            if (!string.Equals(cityName, town.Name))
                throw new InvalidWebCityException(town.Name, cityName);
        }
    }
}