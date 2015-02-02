using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TelemaksRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TelemaksRu
{
    internal class TelemaksRuRazdelPageContentParser : TelemaksRuHtmlPageContentParser, ITelemaksRuWebPageContentParser
    {
        public TelemaksRuWebPageType PageType
        {
            get { return TelemaksRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode catalogsList =
                content.GetSingleNode(@"//div[@class='GroupContainer']");
            HtmlNodeCollection catalogNodes =
                catalogsList.GetNodes(@".//a[@class='update']");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogNode in catalogNodes)
            {
                Uri uri = catalogNode.GetUri(page);
                string name = catalogNode.GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);

                WebPage catalogPage = page.Site.GetPage(uri, TelemaksRuWebPageType.Catalog, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};

        }
    }
}