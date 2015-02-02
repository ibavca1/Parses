using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Chia.WebParsing.Companies.MtsRu;
using HtmlAgilityPack;
using WebParsingFramework;
using WebParsingFramework.Exceptions;

namespace Chia.WebParsing.Parsers.MtsRu
{
    internal class MtsRuRazdelPageContentParser : HtmlPageContentParser, IMtsRuWebPageContentParser
    {
        public MtsRuWebPageType PageType
        {
            get { return MtsRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isListPage = content.HasNode(@"//div[@class='filter_form']");
            if (isListPage)
            {
                WebPage listPage = page.Site.GetPage(page.Uri, MtsRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(listPage);
            }

            HtmlNode categoriesList =
                content.GetSingleNode(@"//div[@class='products_block']");
            HtmlNodeCollection categoriesNodes =
                categoriesList.SelectNodes(@"//div[@class='section_separator']/h4/a");
            var pages = new List<WebPage>();
            foreach (HtmlNode categoryNode in categoriesNodes)
            {
                string name = categoryNode.GetInnerText();
                name = ExtractName(name);
                Uri uri = categoryNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage categoryPage = page.Site.GetPage(uri, MtsRuWebPageType.Catalog, path);
                categoryPage.IsPartOfSiteMap = true;
                pages.Add(categoryPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static string ExtractName(string name)
        {
            Match match = Regex.Match(name, "«(.*)»");
            if (!match.Success)
                throw new InvalidWebPageMarkupException();
            return match.Groups[1].Value;
        }
    }
}