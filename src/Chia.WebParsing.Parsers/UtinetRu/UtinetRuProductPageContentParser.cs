using System;
using Chia.WebParsing.Companies.UtinetRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UtinetRu
{
    internal class UtinetRuProductPageContentParser : UtinetRuHtmlPageContentParser, IUtinetRuWebPageContentParser
    {
        public UtinetRuWebPageType PageType
        {
            get { return UtinetRuWebPageType.Product; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            // бывает, что при заходе в карточку переадресуется на странцу раздела/каталога
            // в этом случае ничего не делаем
            bool isProductPage = content.HasNode(@"//div[@class='b-product-card-menu-wrapper']");
            if (!isProductPage)
                return WebPageContentParsingResult.Empty;

            string name = content
                .GetSingleNode(@"//div[@class='header-page-title' or contains(@class,'b-product-card')]/h1")
                    .GetInnerText();
            string characteristics = content
                .SelectSingleNode(@"//dt[text()='Код производителя']/following-sibling::dd/span")
                .GetInnerText();
            name = MakeName(name, characteristics);
            decimal price = 0;

            bool isAvailable = content.DoesNotHaveNode(@"//*[contains(@class,'unavailable-text')]");
            if (isAvailable)
            {
                price = content
                    .GetSingleNode(@"//h2[contains(@class,'price')]")
                    .GetPrice();
            }

            var product = new WebMonitoringPosition(null, name, price, page.Uri)
                              {
                                  Characteristics = characteristics
                              };
            return WebPageContentParsingResult.FromPosition(product);
        }

       
    }
}