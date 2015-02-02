using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.OzonRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.OzonRu
{
    internal class OzonRuMainPageContentParser : OzonRuHtmlPageContentParser, IOzonRuWebPageContentParser
    {
        public OzonRuWebPageType PageType
        {
            get { return OzonRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//div[@class='eFirstLevelMenu']");
            HtmlNodeCollection razdelsNodes =
                menuNode.GetNodes(@".//li[not(contains(@class,'Promo'))]");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string menuItemName =
                    razdelNode.GetSingleNode(@".//a[@class='eMenuItemLink']").GetInnerText();
                Uri menuItemUri =
                    razdelNode.GetSingleNode(@".//a[@class='eMenuItemLink']").GetUri(page);
                var pagePath = new WebSiteMapPath(menuItemName);

                var razdelPage = page.Site.GetPage(menuItemUri, OzonRuWebPageType.Razdel, pagePath);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}