using System;
using Chia.WebParsing.Companies.DostavkaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DostavkaRu
{
    internal class DostavkaRuProductPageContentParser : HtmlPageContentParser, IDostavkaRuWebPageContentParser
    {
        public DostavkaRuWebPageType PageType
        {
            get { return DostavkaRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            string name = content
                .GetSingleNode(@"//div[@class='product-card']/h1")
                .GetInnerText();
            string article = content
                .GetSingleNode(@"//div[contains(@class,'padding10 kod')]/span[not(contains(@class,'show-special-info'))]")
                .GetDigitsText();
            decimal price = 0;

            bool isAvailable = content.HasNode(@"//link[@href='http://schema.org/InStock']");
            if (isAvailable)
            {
                price = content
                    .GetSingleNode(@"//div[@class='cost']")
                    .GetPrice();
            }

            var position = new WebMonitoringPosition(article, name, price, page.Uri);
            return WebPageContentParsingResult.FromPosition(position);
        }
    }
}