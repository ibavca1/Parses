using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.EnterRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.EnterRu
{
    internal class EnterRuRazdelPageContentParser : EnterRuHtmlPageContentParser, IEnterRuWebPageContentParser
    {
        public EnterRuWebPageType PageType
        {
            get { return EnterRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.HasNode(@"//div[@id='bCatalog']");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, EnterRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode razdelsList =
                content.GetSingleNode(@"//ul[contains(@class,'bCatalogRoot')]");
            HtmlNodeCollection menuItemsNodes =
                razdelsList.GetNodes(@".//li[@class='bCatalogRoot__eItem']/a");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuItemNode in menuItemsNodes)
            {
                Uri uri = menuItemNode.GetUri(page);
                string name = menuItemNode
                    .GetSingleNode(".//div[@class='bCatalogRoot__eNameLink']")
                    .GetInnerText();

                WebSiteMapPath path = page.Path.Add(name);
                WebPage razdelPage = page.Site.GetPage(uri, EnterRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult { Pages = pages };
        }
    }
}