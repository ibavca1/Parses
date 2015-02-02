using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.Oo3Ru;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.Oo3Ru
{
    internal class Oo3RuMainPageContentParser : Oo3RuHtmlPageContentParser, IOo3RuWebPageContentParser
    {
        public Oo3RuWebPageType PageType
        {
            get { return Oo3RuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//ul[@class='menu_1']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//li[contains(@class,'item_')]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                Uri uri = razdelNode.GetUri(page);
                string name = razdelNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                var razdelPage = page.Site.GetPage(uri, Oo3RuWebPageType.Catalog, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}