using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.TdPoiskRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.TdPoiskRu
{
    internal class TdPoiskRuRazdelPageContentParser : TdPoiskRuHtmlPageContentParser, ITdPoiskRuWebPageContentParser
    {
        public TdPoiskRuWebPageType PageType
        {
            get { return TdPoiskRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//form[@id='filter_form']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, TdPoiskRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode categoriesList =
                content.GetSingleNode(@"//div[@id='categories_list']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.GetNodes(@".//li[@class='b-categories-list__item']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode categoriesNode in categoriesNodes)
            {
                string name = categoriesNode.GetInnerText();
                Uri uri = categoriesNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, TdPoiskRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}