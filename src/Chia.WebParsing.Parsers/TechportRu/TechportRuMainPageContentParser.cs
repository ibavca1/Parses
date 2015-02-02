using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TechportRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TechportRu
{
    internal class TechportRuMainPageContentParser : TechportRuHtmlPageContentParser, ITechportRuWebPageContentParser
    {
        public TechportRuWebPageType PageType
        {
            get { return TechportRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNodeCollection razdelsNodes =
                content.GetNodes(@".//span[@class='eshop_sm_level_1']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, TechportRuWebPageType.Catalog, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}