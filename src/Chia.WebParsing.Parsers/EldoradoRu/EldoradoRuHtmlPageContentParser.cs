using System.Collections.Generic;
using Chia.WebParsing.Companies.EldoradoRu;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.EldoradoRu
{
    internal abstract class EldoradoRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            HtmlNode cityNode =
                content.GetSingleNode(@"//a[contains(@class,'headerRegionName')]");
            string cityName = cityNode.GetInnerText();
            EldoradoRuCity town = EldoradoRuCity.Get(page.Site.City);

            if (!string.Equals(cityName, town.Name))
                throw new InvalidWebCityException(town.Name, cityName);
        }

        public virtual IDictionary<string, decimal> ParseProductPrices(WebPageContent content)
        {
            var result = new Dictionary<string, decimal>();

            string source = content.ReadAsString();
            JToken items = JObject.Parse(source)["items"];

            foreach (var jToken in items.Children())
            {
                var token = (JProperty)jToken;
                string button = token.Value["button"].ToString();
                string bid = token.Value["bid"].ToString();
                int price = int.Parse(token.Value["productPriceLocal"].ToString());

                if (!"available".Equals(button))
                    price = 0;

                result.Add(bid, price);
            }

            return result;
        }
    }
}