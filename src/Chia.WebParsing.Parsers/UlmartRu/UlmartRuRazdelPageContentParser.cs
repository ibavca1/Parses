using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.UlmartRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal class UlmartRuRazdelPageContentParser : HtmlPageContentParser, IUlmartRuWebPageContentParser
    {
        public UlmartRuWebPageType PageType
        {
            get { return UlmartRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            HtmlNode categoriesList =
                content.GetSingleNode(@"//section[@class='h-sect-margin1-bottom']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//li[@class='b-list__item b-list__item_bigger']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, UlmartRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}