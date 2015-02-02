using System;
using Chia.WebParsing.Companies.OnlinetradeRu;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OnlinetradeRu
{
    internal class OnlinetradeRuMainPageContentParser : OnlinetradeRuHtmlPageContentParser, IOnlinetradeRuWebPageContentParser
    {
        public OnlinetradeRuWebPageType PageType
        {
            get { return OnlinetradeRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            Uri razdelsUri = content
                .GetSingleNode(@"//a[text()='Каталог']")
                .GetUri(page);
            WebPage razdelsPage = page.Site.GetPage(razdelsUri, OnlinetradeRuWebPageType.RazdelsList, page.Path);
            razdelsPage.IsPartOfSiteMap = true;
            return WebPageContentParsingResult.FromPage(razdelsPage);
        }
    }
}