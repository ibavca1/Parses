using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.SvyaznoyRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.SvyaznoyRu
{
    internal class SvyaznoyRuRazdelPageContentParser : SvyaznoyRuHtmlPageContentParser, ISvyaznoyRuWebPageContentParser
    {
        public SvyaznoyRuWebPageType PageType
        {
            get { return SvyaznoyRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@id='filterUserType_new']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, SvyaznoyRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            WebPageContentParsingResult result;

            if (TryParseAsType1(page, content, out result))
                return result;

            if (TryParseAsType2(page, content, out result))
                return result;

            throw new InvalidWebPageMarkupException();
        }

        private static bool TryParseAsType1(WebPage page, HtmlPageContent content, out WebPageContentParsingResult result)
        {
            HtmlNode categoriesContainerNode =
               content.SelectSingleNode(@"//ul[@class='cat-list cleared']");
            if (categoriesContainerNode == null)
            {
                result = null;
                return false;
            }

            HtmlNodeCollection categoriesNodes =
                categoriesContainerNode.GetNodes(@"li/div[@class='name-cat']/a");
            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                var categoryPage = page.Site.GetPage(uri, SvyaznoyRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            result = new WebPageContentParsingResult { Pages = pages };
            return true;
        }
    
        private static bool TryParseAsType2(WebPage page, HtmlPageContent content, out WebPageContentParsingResult result)
        {
            HtmlNode categoriesContainerNode =
                content.SelectSingleNode(@"//ul[@class='accBl_links cleared']");
            if (categoriesContainerNode == null)
            {
                result = null;
                return false;
            }

            HtmlNodeCollection categoriesNodes =
                categoriesContainerNode.GetNodes(@"li/a");
            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                var categoryPage = page.Site.GetPage(uri, SvyaznoyRuWebPageType.Razdel, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            result = new WebPageContentParsingResult { Pages = pages };
            return true;
        }
    
    }
}