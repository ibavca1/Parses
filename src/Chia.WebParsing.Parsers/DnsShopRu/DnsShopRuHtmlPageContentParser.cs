using System;
using Chia.WebParsing.Companies.DnsShopRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.DnsShopRu
{
    internal abstract class DnsShopRuHtmlPageContentParser : HtmlPageContentParser
    {
        protected override void ValidateContent(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            base.ValidateContent(page, content, context);

            HtmlNode cityNode =
                content.GetSingleNode(@"//a[@id='region_nav_selector']");
            string cityName = cityNode.GetInnerText();
            DnsShopRuCity city = DnsShopRuCity.Get(page.Site.City);

            bool areCitiesEqual =
                string.Equals(cityName, city.Name, StringComparison.InvariantCultureIgnoreCase);
            if (!areCitiesEqual)
                throw new InvalidWebCityException(city.Name, cityName);
        }
    }
}