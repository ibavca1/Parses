using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.HolodilnikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.HolodilnikRu
{
    internal class HolodilnikRuMainPageContentParser : HolodilnikRuHtmlPageContentParser, IHolodilnikRuWebPageContentParser
    {
        public virtual HolodilnikRuWebPageType PageType
        {
            get { return HolodilnikRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode menuNode =
                content.GetSingleNode(@"//div[@class='nav']");
            HtmlNodeCollection razdelsNodes =
                menuNode.GetNodes(".//a[contains(@class,'dDrop')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                var path = new WebSiteMapPath(name);

                WebPage razdelPage = page.Site.GetPage(uri, HolodilnikRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}