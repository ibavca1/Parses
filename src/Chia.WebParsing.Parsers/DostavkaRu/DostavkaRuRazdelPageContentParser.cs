using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.DostavkaRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DostavkaRu
{
    internal class DostavkaRuRazdelPageContentParser : HtmlPageContentParser, IDostavkaRuWebPageContentParser
    {
        public DostavkaRuWebPageType PageType
        {
            get { return DostavkaRuWebPageType.Razdel; }
        }

        public override WebPageContentParsingResult ParseHtml(
            WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            bool isCatalogPage = content.DoesNotHaveNode(@"//h3[@category_id]");
            if (isCatalogPage)
            {
                WebPage catalogPage = page.Site.GetPage(page.Uri, DostavkaRuWebPageType.Catalog, page.Path);
                return WebPageContentParsingResult.FromPage(catalogPage);
            }

            HtmlNode razdelsList =
               content.GetSingleNode(@"//div[@id='SubCategoriesBlock']");
            HtmlNodeCollection razdelsNodes =
                razdelsList.SelectNodes(@".//h3[@category_id]//a[@class='orange']");

            var pages = new List<WebPage>();
            foreach (HtmlNode razdelNode in razdelsNodes)
            {
                string name = razdelNode.GetInnerText();
                Uri uri = razdelNode.GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);

                WebPage razdelPage = page.Site.GetPage(uri, DostavkaRuWebPageType.Razdel, path);
                razdelPage.IsPartOfSiteMap = true;
                pages.Add(razdelPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}