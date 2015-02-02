using Chia.WebParsing.Companies.MvideoRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal abstract class MvideoRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            HtmlNode cityNode =
                content.GetSingleNode(@"//label[contains(@for,'city-radio')]");
            string cityName = cityNode.GetInnerText();
            MvideoRuCity town = MvideoRuCity.Get(page.Site.City);

            if (!string.Equals(cityName, town.Name))
                throw new InvalidWebCityException(town.Name, cityName);
        }
    }
}