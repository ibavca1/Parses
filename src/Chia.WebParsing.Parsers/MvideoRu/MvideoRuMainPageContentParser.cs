using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.MvideoRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.MvideoRu
{
    internal class MvideoRuMainPageContentParser : MvideoRuHtmlPageContentParser, IMvideoRuWebPageContentParser
    {
        public MvideoRuWebPageType Type
        {
            get {  return MvideoRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = new List<WebPage>();

            Uri razdelsUri = content
                .GetSingleNode(@".//a[text()='Каталог товаров']")
                .GetUri(page);
            WebPage razdelsPage = page.Site.GetPage(razdelsUri, MvideoRuWebPageType.RazdelsList, page.Path);
            razdelsPage.IsPartOfSiteMap = true;
            pages.Add(razdelsPage);

            if (context.Options.InformationAboutShops)
            {
                Uri shopsUri = 
                    content.GetSingleNode(@"//a[text()='Все магазины']").GetUri(page);
                WebPage shopsPage = page.Site.GetPage(shopsUri, MvideoRuWebPageType.ShopsList);
                shopsPage.IsPartOfShopsInformation = true;
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}