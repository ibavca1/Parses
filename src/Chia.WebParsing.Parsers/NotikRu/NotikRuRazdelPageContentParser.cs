using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.NotikRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.NotikRu
{
    internal class NotikRuRazdelPageContentParser : NotikRuHtmlPageContentParser, INotikRuWebPageContentParser
    {
        public NotikRuWebPageType PageType
        {
            get { return NotikRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode categoriesList =
                content.GetSingleNode(@"//div[@class='content']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//div[@class='goodscateg']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, NotikRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}