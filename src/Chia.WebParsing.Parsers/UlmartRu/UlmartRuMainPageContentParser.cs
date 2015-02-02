using System;
using System.Collections.Generic;
using System.Linq;
using Chia.WebParsing.Companies.UlmartRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.UlmartRu
{
    internal class UlmartRuMainPageContentParser : HtmlPageContentParser, IUlmartRuWebPageContentParser
    {
        public UlmartRuWebPageType PageType
        {
            get { return UlmartRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            var pages = ParseMainMenu(page, content).ToList();

            if (context.Options.InformationAboutShops)
            {
                WebPage shopsPage = ParseShopsPage(page, content);
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

        private static IEnumerable<WebPage> ParseMainMenu(WebPage page, HtmlPageContent content)
        {
            HtmlNode razdelsList =
                content.GetSingleNode(@"//ul[contains(@class,'b-list_catalog-menu')]");
            HtmlNodeCollection razdelsNodes =
                razdelsList.GetNodes(@".//a[@href and contains(@class,'b-dropdown__handle')]");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                Uri uri = razdelNode.GetUri(page);
                string name = razdelNode.GetSingleNode(@"text()").GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, UlmartRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return pages;
        }

        private static WebPage ParseShopsPage(WebPage page, HtmlPageContent content)
        {
            Uri shopsUri = content
                .GetSingleNode(@"//a[contains(text(),'Адреса и контакты')]")
                .GetUri(page);
            WebPage shopsPage = page.Site.GetPage(shopsUri, UlmartRuWebPageType.ShopsList, page.Path);
            shopsPage.IsPartOfShopsInformation = true;
            return shopsPage;
        }

        }
}