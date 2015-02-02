using System.Globalization;
using Chia.WebParsing.Companies.VLazerCom;
using Newtonsoft.Json;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.VLazerCom
{
    internal abstract class VLazerComHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            if (content.IsJavaScriptProcessed)
            {
                string expectedCityName = VLazerComCity.Get(page.Site.City).Name;
                string actualCityName =
                    content.GetSingleNode(@"//li[contains(@class,'basebox city')]/span[@class='name']").GetInnerText();

                bool areCitiesCorrect =
                    string.Equals(expectedCityName, actualCityName);
                if (!areCitiesCorrect)
                    throw new InvalidWebCityException(expectedCityName, actualCityName);
            }
        }

        internal virtual decimal GetPriceOffset(WebPage page, HtmlPageContent document, WebPageContentParsingContext context)
        {
            string offsetKey = document
                .GetSingleNode(@"//div[@class='none jp_offset']")
                .GetInnerText();

            WebPageRequest request = WebPageRequest.Create(VLazerComWebPageType.PriceOffset, EmptyUri.Value);
            request.Cookies = page.Cookies;
            request.Referer = page.Uri.ToString();
            request.Content = new StringWebPageContent(offsetKey);
            WebPageContent content = page.Site.LoadPageContent(request, context);

            string json = content.ReadAsString();
            dynamic obj = JsonConvert.DeserializeObject(json);
            string offsetValue = obj.offset;
            decimal offset = decimal.Parse(offsetValue, NumberStyles.Number, CultureInfo.InvariantCulture);
            return offset;
        }
    }
}