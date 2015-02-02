using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.DostavkaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DostavkaRu
{
    internal class DostavkaRuMainPageContentParser : HtmlPageContentParser, IDostavkaRuWebPageContentParser
    {
        public DostavkaRuWebPageType PageType
        {
            get { return DostavkaRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//div[@id='CatalogNavigation']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//div[@rel]/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, DostavkaRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}