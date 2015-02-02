using Chia.WebParsing.Companies.CentrBtRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CentrBtRu
{
    internal class CentrBtRuRazdelPageContentParser : CentrBtRuMainPageContentParser
    {
        public override CentrBtRuWebPageType PageType
        {
            get { return CentrBtRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//img[@src='pic/button_basket.gif']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, CentrBtRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            return ParseRazdels(page, content);
        }
    }
}