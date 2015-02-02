using Chia.WebParsing.Companies.ElectrovenikRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    internal class ElectrovenikRuRazdelPageContentParser : ElectrovenikRuMainPageContentParser
    {
        public override ElectrovenikRuWebPageType PageType
        {
            get { return ElectrovenikRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//form[@id='filter_form']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, ElectrovenikRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            return base.ParseHtml(page, content, context);
        }
    }
}