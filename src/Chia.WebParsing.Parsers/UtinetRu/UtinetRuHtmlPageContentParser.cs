using System;
using Chia.WebParsing.Companies.UtinetRu;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    internal abstract class UtinetRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = UtinetRuCity.Get(page.Site.City).Name;
            string actualCityName =
                content.GetSingleNode(@"//span[contains(@id,'chooseRegion')]").GetInnerText();

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }

        protected static string MakeName(string name, string manufacturerCode)
        {
            if (!string.IsNullOrWhiteSpace(manufacturerCode) &&
                    name.IndexOf(manufacturerCode, StringComparison.InvariantCultureIgnoreCase) == -1)
            {
                return string.Format("{0} $$ {1}", name, manufacturerCode);
            }

            return name;
        }
    }
}