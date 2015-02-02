using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.KeyRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.KeyRu
{
    internal class KeyRuMainPageContentParser: KeyRuHtmlPageContentParser, IKeyRuWebPageContentParser
    {
        public KeyRuWebPageType Type
        {
            get { return KeyRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content,
            WebPageContentParsingContext context)
        {
            HtmlNodeCollection catalogsNodes =
                content.GetNodes(@"//div[@data-branch_id]");

            var pages = new List<WebPage>();
            foreach (HtmlNode catalogsNode in catalogsNodes)
            {
                string name = catalogsNode
                    .GetSingleNode(@".//div[@class='title_line']/a/span")
                    .GetInnerText();
                Uri uri = catalogsNode
                    .SelectSingleNode(@".//div[@class='title_line']/a")
                    .GetUri(page);
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, KeyRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            if (context.Options.InformationAboutShops)
            {
                Uri shopsPageUri = content
                    .SelectSingleNode(@"//div[@class='header_city_label']/div[@class='second_line']/a")
                    .GetUri(page);
                WebPage shopsPage = page.Site.GetPage(shopsPageUri, KeyRuWebPageType.ShopsList);
                shopsPage.IsPartOfShopsInformation = true;
                pages.Add(shopsPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }

    }
}