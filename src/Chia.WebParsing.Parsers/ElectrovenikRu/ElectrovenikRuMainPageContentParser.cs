using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.ElectrovenikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.ElectrovenikRu
{
    internal class ElectrovenikRuMainPageContentParser : ElectrovenikRuHtmlPageContentParser, IElectrovenikRuWebPageContentParser
    {
        public virtual ElectrovenikRuWebPageType PageType
        {
            get { return ElectrovenikRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNodeCollection razdelsNodes =
               content.GetNodes(@"//ul/li//a[@class='cat-link']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razldePage = page.Site.GetPage(uri, ElectrovenikRuWebPageType.Razdel, path);
                razldePage.IsPartOfSiteMap = true;
                pages.Add(razldePage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}