using System;
using Chia.WebParsing.Companies.CitilinkRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.CitilinkRu
{
    internal class CitilinkRuMainPageContentParser : CitilinkRuHtmlPageContentParser, ICitilinkRuWebPageContentParser
    {
        public CitilinkRuWebPageType PageType
        {
            get { return CitilinkRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode headerMenu =
                content.GetSingleNode(@"//div[@class='header']");
            HtmlNode catalogNode =
                headerMenu.GetSingleNode(@".//ul/li/a[@title='Товары']");

            Uri catalogUri = catalogNode.GetUri(page);
            WebPage catalogPage = page.Site.GetPage(catalogUri, CitilinkRuWebPageType.Razdel, page.Path);
            catalogPage.IsPartOfSiteMap = true;
            return WebPageContentParsingResult.FromPage(catalogPage);
        }
    }
}