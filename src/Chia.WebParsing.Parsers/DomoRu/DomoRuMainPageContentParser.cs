using System;
using System.Collections.Generic;
using Chia.WebParsing.Companies.DomoRu;
using HtmlAgilityPack;
using WebParsingFramework;

namespace Chia.WebParsing.Parsers.DomoRu
{
    internal class DomoRuMainPageContentParser : DomoRuHtmlPageContentParser, IDomoRuWebPageContentParser
    {
        public DomoRuWebPageType Type
        {
            get { return DomoRuWebPageType.Main; }
        }

        public override WebPageContentParsingResult ParseHtml(WebPage page, HtmlPageContent content, WebPageContentParsingContext context)
        {
            WebPage mainMenuPage = page.Site.GetPage(EmptyUri.Value, DomoRuWebPageType.MainMenuAjax, page.Path);
            WebPageRequest mainMenuPageRequest = WebPageRequest.Create(mainMenuPage);
            HtmlPageContent mainMenuContent = HtmlPageContent.Create(page.Site.LoadPageContent(mainMenuPageRequest, context));

            HtmlNode mainMenuNode =
                mainMenuContent.GetSingleNode(@"//div[@class='departments']");
            HtmlNodeCollection mainMenuNodes =
                mainMenuNode.GetNodes(@".//div[@class='department']");

            var pages = new List<WebPage>();
            foreach (HtmlNode menuNode in mainMenuNodes)
            {
                Uri uri = menuNode
                    .GetSingleNode(@".//a[contains(@id,'TopCategoryLink')]")
                    .GetUri(page);
                string name = menuNode
                    .GetSingleNode(@".//a[contains(@id,'TopCategoryLink')]/text()")
                    .GetInnerText();
                WebSiteMapPath path = page.Path.Add(name);
                WebPage catalogPage = page.Site.GetPage(uri, DomoRuWebPageType.Razdel, path);
                catalogPage.IsPartOfSiteMap = true;
                pages.Add(catalogPage);
            }

            return new WebPageContentParsingResult {Pages = pages};
        }
    }
}