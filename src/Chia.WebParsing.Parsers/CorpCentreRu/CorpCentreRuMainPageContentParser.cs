using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.CorpCentreRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CorpCentreRu
{
    internal class CorpCentreRuMainPageContentParser : CorpCentreRuHtmlPageContentParser, ICorpCentreRuWebPageContentParser
    {
        public CorpCentreRuWebPageType PageType
        {
            get { return CorpCentreRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            Uri catalogUri =
                content.GetSingleNode(@"//a[text()='Каталог техники']").GetUri(page);
            WebPage catalogPage = page.Site.GetPage(catalogUri, CorpCentreRuWebPageType.RazdelsList, page.Path);
            catalogPage.IsPartOfSiteMap = true;
            pages.Add(catalogPage);

            if (context.Options.InformationAboutShops)
            {
                Uri shopsUri =
                    content.GetSingleNode(@"//a[text()='Ближайшие магазины']").GetUri(page);
                WebPage shopsPage = page.Site.GetPage(shopsUri, CorpCentreRuWebPageType.ShopsList, page.Path);
                shopsPage.IsPartOfShopsInformation = true;
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}