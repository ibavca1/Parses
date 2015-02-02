using Chia.WebParsing.Companies.UlmartRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal class UlmartRuShopPageContentParser : HtmlPageContentParser, IUlmartRuWebPageContentParser
    {
        public UlmartRuWebPageType PageType
        {
            get { return UlmartRuWebPageType.Shop; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode nameNode =
                content.SelectSingleNode(@"//a[contains(text(),'веб-трансляция')]/preceding-sibling::strong") ??
                content.SelectSingleNode(@"//a[contains(text(),'веб-трансляция')]/../preceding-sibling::h3");

            string name = nameNode.WithValidate().GetInnerText();
            
            WebShop shop = new WebShop(name);
            return WebPageContentParsingResult.FromShop(shop);
        }
    }
}