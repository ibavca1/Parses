using System;
using System.Text.RegularExpressions;
using Chia.WebParsing.Companies.RbtRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.RbtRu
{
    internal abstract class RbtRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            string expectedCityName = RbtRuCity.Get(page.Site.City).Name;
            string actualCityName = GetCityName(content);

            bool areCitiesCorrect =
                string.Equals(expectedCityName, actualCityName);
            if (!areCitiesCorrect)
                throw new InvalidWebCityException(expectedCityName, actualCityName);
        }

        private static string GetCityName(HtmlPageContent content)
        {
            HtmlNode locationNode =
                content.GetSingleNode(@"//div[@id='location']");
            HtmlNode cityNode =
                locationNode.SelectSingleNode(@"a/h2");
            if (cityNode == null)
            {
                string dataKey = locationNode.GetSingleNode(@"span").GetAttributeText("data-key");
                string pattern = string.Format(@"'{0}': '(\S+)'", dataKey);

                Match match = Regex.Match(content.ReadAsString(), pattern);
                if (!match.Success)
                    throw new InvalidWebPageMarkupException();

                string cityEncodedHtml = match.Groups[1].Value;
                string cityHtml = content.Encoding.GetString(Convert.FromBase64String(cityEncodedHtml));
                locationNode.InnerHtml = cityHtml;
                cityNode =
                    locationNode.GetSingleNode(@"a/h2");
            }

            return cityNode.GetInnerText();
        }
    }
}